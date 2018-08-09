using System;
using System.IO;
using LogsParser.Common;
using LogsParser.Services.Contracts;
using LogsParser.Web.Models;
using Microsoft.AspNet.SignalR;

namespace LogsParser.Web.Hubs
{
    public class MatchHub : Hub
    {
        private readonly IDirectoryService directoryService;
        private readonly IHttpCacheService cacheService;
        private readonly IParseService parseService;

        public MatchHub(IDirectoryService directoryService,
            IHttpCacheService cacheService,
            IParseService parseService)
        {
            this.directoryService = directoryService;
            this.cacheService = cacheService;
            this.parseService = parseService;
        }

        public void FindMatch(string folder, string pattern)
        {
            var selectedDirectory = Path.Combine(LogsParserConstants.DefaultLogsFolder, folder);

            var files = cacheService.Get(
                selectedDirectory,
                () => directoryService.GetLogFilesPerDirectory(selectedDirectory),
                60);

            foreach (var file in files)
            {
                var allFileLines = File.ReadAllLines(file);

                for (int i = 0; i < allFileLines.Length; i++)
                {
                    var row = allFileLines[i].IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase);
                    if (row >= 0)
                    {
                        var model = new MatchModel()
                                        {
                                            Path = $"{folder}/{Path.GetFileName(file)}",
                                            Col = i,
                                            Row = row
                                        };
                        Clients.All.addLinkForMatch(model);
                    }
                }
            }
        }
    }
}
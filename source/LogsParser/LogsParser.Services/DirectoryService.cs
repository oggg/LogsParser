using System.Collections.Generic;
using System.IO;
using System.Linq;
using LogsParser.Common;
using LogsParser.Services.Contracts;

namespace LogsParser.Services
{
    public class DirectoryService : IDirectoryService
    {
        public IEnumerable<string> GetLogDirectories()
        {
            return Directory.EnumerateDirectories(LogsParserConstants.DefaultLogsFolder)
                    .Select(x => x.Remove(0, LogsParserConstants.DefaultLogsFolder.Length));
        }

        public IEnumerable<string> GetLogFilesPerDirectory(string path)
        {
            return Directory.EnumerateFiles(Path.Combine(LogsParserConstants.DefaultLogsFolder, path));
        }
    }
}

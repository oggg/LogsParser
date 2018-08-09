using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using LogsParser.Common;
using LogsParser.Services.Contracts;
using LogsParser.Web.Models;

namespace LogsParser.Services
{
    public class ParseService : IParseService
    {
        public ICollection<MatchModel> FindMatches(IEnumerable<string> fileCollection, string pattern)
        {
            var result = new List<MatchModel>();

            foreach (var file in fileCollection)
            {
                var allFileLines = File.ReadAllLines(file);

                for (int i = 0; i < allFileLines.Length; i++)
                {
                    var row = allFileLines[i].IndexOf(pattern, StringComparison.CurrentCultureIgnoreCase);
                    if (row >= 0)
                    {
                        result.Add(new MatchModel()
                        {
                            Path = file,
                            Col = i,
                            Row = row
                        });
                    }
                }
            }

            return result;
        }

        public string GetPidContent(MatchModel matchModel)
        {
            var fileToParse = Path.Combine(LogsParserConstants.DefaultLogsFolder, matchModel.Path);

            bool previousPidFound = false;
            bool nextPidFound = false;

            var pidTextList = new LinkedList<string>();
            var searchPatternLine = File.ReadLines(fileToParse).Skip(matchModel.Row).Take(1).First();
            var pidIndexOnSearchPatternLine = searchPatternLine.IndexOf(LogsParserConstants.PID);

            if (pidIndexOnSearchPatternLine >= 0)
            {
                if (pidIndexOnSearchPatternLine < matchModel.Row)
                {
                    previousPidFound = true;
                }

                if (pidIndexOnSearchPatternLine > matchModel.Row)
                {
                    nextPidFound = true;
                }
            }
            pidTextList.AddFirst(searchPatternLine);

            for (int i = 0; i < matchModel.Row; i++)
            {
                if (previousPidFound && nextPidFound)
                {
                    break;
                }

                if (!previousPidFound)
                {
                    var previousLine = File.ReadLines(fileToParse).Skip(matchModel.Row - i).Take(1).First();
                    if (previousLine.Contains(LogsParserConstants.PID))
                    {
                        previousPidFound = true;
                    }

                    pidTextList.AddFirst(previousLine);
                }

                if (!nextPidFound)
                {
                    var nextLine = File.ReadLines(fileToParse).Skip(matchModel.Row + i).Take(1).First();
                    if (nextLine.Contains(LogsParserConstants.PID))
                    {
                        nextPidFound = true;
                    }
                    else
                    {
                        pidTextList.AddLast(nextLine);
                    }
                }
            }

            // var pidText = HttpContext.Current.Server.HtmlEncode(string.Concat(pidTextList));
            var pidText = string.Concat(pidTextList);
            return pidText;
        }
    }
}

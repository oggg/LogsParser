using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using LogsParser.Common;
using LogsParser.Services.Contracts;
using LogsParser.Models;

namespace LogsParser.Services
{
    public class ParseService : IParseService
    {
        public string GetPidContent(MatchModel matchModel)
        {
            var fileToParse = Path.Combine(LogsParserConstants.DefaultLogsFolder, matchModel.Path);

            bool previousPidFound = false;
            bool nextPidFound = false;
            
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

            var pidTextList = new LinkedList<string>();
            pidTextList.AddFirst(searchPatternLine + Environment.NewLine);

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

                    pidTextList.AddFirst(previousLine + Environment.NewLine);
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
                        pidTextList.AddLast(nextLine + Environment.NewLine);
                    }
                }
            }

            return string.Concat(pidTextList);
        }
    }
}

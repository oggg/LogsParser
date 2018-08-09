using System.Collections.Generic;
using LogsParser.Web.Models;

namespace LogsParser.Services.Contracts
{
    public interface IParseService
    {
        ICollection<MatchModel> FindMatches(IEnumerable<string> fileCollection, string pattern);

        string GetPidContent(MatchModel matchModel);
    }
}

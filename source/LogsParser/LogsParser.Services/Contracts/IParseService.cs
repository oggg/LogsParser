using System.Collections.Generic;
using LogsParser.Web.Models;

namespace LogsParser.Services.Contracts
{
    public interface IParseService
    {
        string GetPidContent(MatchModel matchModel);
    }
}

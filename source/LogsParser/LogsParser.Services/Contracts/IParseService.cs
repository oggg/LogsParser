using LogsParser.Models;

namespace LogsParser.Services.Contracts
{
    public interface IParseService
    {
        string GetPidContent(MatchModel matchModel);
    }
}

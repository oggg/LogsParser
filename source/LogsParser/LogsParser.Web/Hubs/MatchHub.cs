using LogsParser.Web.Models;
using Microsoft.AspNet.SignalR;

namespace LogsParser.Web.Hubs
{
    public class MatchHub : Hub
    {
        public void FoundMatch(string folderAndFileName, int row, int col)
        {
            Clients.All.addLinkForMatch(new MatchModel()
            {
                Path = folderAndFileName,
                Col = col,
                Row = row
            });
        }
    }
}
using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using LogsParser.Common;
using LogsParser.Services.Contracts;
using LogsParser.Models;

namespace LogsParser.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IParseService parseService;

        public SearchController(IParseService parseService)
        {
            this.parseService = parseService;
        }

        public ActionResult StringMatches()
        {
            return View();
        }

        public ActionResult GetPid(MatchModel matchModel)
        {
            var pidText = parseService.GetPidContent(matchModel);

            return this.Content(pidText, "text/plain");
        }
    }
}
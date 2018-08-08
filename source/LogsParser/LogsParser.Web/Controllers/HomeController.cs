using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LogsParser.Common;
using LogsParser.Services.Contracts;
using LogsParser.Web.Models;

namespace LogsParser.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDirectoryService directoryService;
        private readonly IHttpCacheService cacheService;

        public HomeController(IDirectoryService directoryService, IHttpCacheService cacheService)
        {
            this.directoryService = directoryService;
            this.cacheService = cacheService;
        }

        public ActionResult Index()
        {
            var directories = cacheService.Get(
                LogsParserConstants.DirectoriesCacheKey,
                () => directoryService.GetLogDirectories(), 60);

            var formViewModel = new SearchFormModel()
            {
                Directories = directories
            };

            return View(formViewModel);
        }


    }
}
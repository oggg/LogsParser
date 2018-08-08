using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LogsParser.Services.Contracts;
using LogsParser.Web.Models;

namespace LogsParser.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDirectoryService directoryService;

        public HomeController(IDirectoryService directoryService)
        {
            this.directoryService = directoryService;
        }

        public ActionResult Index()
        {
            var directories = directoryService.GetLogDirectories();

            var formViewModel = new SearchFormModel()
            {
                Directories = directories
            };

            return View(formViewModel);
        }
    }
}
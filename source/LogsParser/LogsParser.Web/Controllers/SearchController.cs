using System.Web.Mvc;
using LogsParser.Web.Models;

namespace LogsParser.Web.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public ActionResult StringMatches(SearchFormPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
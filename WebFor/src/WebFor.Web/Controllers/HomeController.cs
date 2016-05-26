using Microsoft.AspNetCore.Mvc;
using WebFor.Core;
using WebFor.Core.Services;
using WebFor.Core.Domain;
using WebFor.Core.Repository;

namespace WebFor.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _db;

        public HomeController(IUnitOfWork uw)
        {
            _db = uw;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Route("/Error/Status/{statusCode?}")]
        public IActionResult Error(int statusCode)
        {
            ViewBag.StatusCode = statusCode;

            return View();
        }
    }
}

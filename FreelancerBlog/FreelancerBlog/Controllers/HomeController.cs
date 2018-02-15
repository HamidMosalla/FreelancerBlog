using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FreelancerBlog.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        [Route("/motherfuck/index")]
        public IActionResult Index()
        {

            throw new System.Exception("This is my exception, don't touch it.");

            return View("Index");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }

        public IActionResult Services()
        {
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
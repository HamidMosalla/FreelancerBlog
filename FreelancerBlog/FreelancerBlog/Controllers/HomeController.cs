using FreelancerBlog.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _uw;

        public HomeController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        public IActionResult Index()
        {
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
            //It gave 404 because of missing css and js map file and missing stylesheet
            ViewBag.StatusCode = statusCode;

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _uw.Dispose();
            base.Dispose(disposing);
        }

    }
}

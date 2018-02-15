using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FreelancerBlog.Controllers
{
    public class HomeController : Controller
    {
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
            ViewBag.StatusCode = statusCode;

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebFor.Repositories;

namespace WebFor.Controllers
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

        public IActionResult Error()
        {
            return View();
        }
    }
}

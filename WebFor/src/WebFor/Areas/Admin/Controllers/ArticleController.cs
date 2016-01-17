using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using WebFor.Models;
using WebFor.Repositories;

namespace WebFor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private IUnitOfWork _db;

        public ArticleController(IUnitOfWork uw)
        {
            _db = uw;    
        }



        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article)
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Article article)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }
    }
}

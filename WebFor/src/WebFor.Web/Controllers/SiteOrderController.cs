using Microsoft.AspNet.Mvc;
using WebFor.Core;
using WebFor.Core.Services;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Web.Services;
using WebFor.Web.ViewModels.SiteOrder;

namespace WebFor.Web.Controllers
{
    public class SiteOrderController : Controller
    {
        [FromServices]
        public IUnitOfWork  _db { get; set; }

        [FromServices]
        public IWebForMapper WebForMapper { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SiteOrderViewModel viewModel)
        {
            return View();
        }
    }
}

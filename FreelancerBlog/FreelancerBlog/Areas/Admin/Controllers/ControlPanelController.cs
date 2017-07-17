using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ControlPanelController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
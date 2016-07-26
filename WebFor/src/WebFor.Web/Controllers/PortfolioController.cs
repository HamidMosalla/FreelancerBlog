using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;
using WebFor.Web.ViewModels.Contact;
using cloudscribe.Web.Pagination;
using WebFor.Web.Mapper;

namespace WebFor.Web.Controllers
{
    public class PortfolioController : Controller
    {
        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;

        public PortfolioController(IUnitOfWork uw, IWebForMapper webForMapper)
        {
            _uw = uw;
            _webForMapper = webForMapper;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id.Equals(default(int)))
            {
                return BadRequest();
            }

            var model = await _uw.PortfolioRepository.FindByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var viewModel = _webForMapper.PortfolioToPorfolioViewModel(model);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _uw.PortfolioRepository.GetAllAsync();

            var viewModel = _webForMapper.PortfolioCollectionToPortfolioViewModelCollection(model);

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _uw.Dispose();
            base.Dispose(disposing);
        }

    }
}

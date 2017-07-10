using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Controllers
{
    public class PortfolioController : Controller
    {
        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;
        private readonly IMapper _mapper;

        public PortfolioController(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper, IMapper mapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
            _mapper = mapper;
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

            var viewModel = _mapper.Map<Portfolio, PortfolioViewModel>(model);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _uw.PortfolioRepository.GetAllAsync();

            var viewModel = _freelancerBlogMapper.PortfolioCollectionToPortfolioViewModelCollection(model);

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            _uw.Dispose();
            base.Dispose(disposing);
        }

    }
}

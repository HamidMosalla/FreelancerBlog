using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using FreelancerBlog.Web.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.Web.Features.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Web.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PortfolioController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id == default(int)) return BadRequest();

            var model = await _mediator.Send(new PortfolioByIdQuery { PortfolioId = id });

            if (model == null) return NotFound();

            var viewModel = _mapper.Map<Portfolio, PortfolioViewModel>(model);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var portfolios = await _mediator.Send(new GetAllPortfoliosQuery());

            var viewModel = _mapper.Map<IQueryable<Portfolio>, List<PortfolioViewModel>>(portfolios);

            await _mediator.Send(new PopulatePortfolioCategoryListCommand { Portfolios = portfolios, ViewModel = viewModel });

            return View(viewModel);
        }
    }
}
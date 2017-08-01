using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Controllers
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

            var model = await _mediator.Send(new PortfolioByIdQuery {PortfolioId = id});

            if (model == null) return NotFound();

            var viewModel = _mapper.Map<Portfolio, PortfolioViewModel>(model);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var portfolios = await _mediator.Send(new GetAllPortfoliosQuery());

            var viewModel = _mapper.Map<List<Portfolio>, List<PortfolioViewModel>>(portfolios.ToList());

            viewModel.ForEach(v => v.PortfolioCategoryList = portfolios.Single(p => p.PortfolioId.Equals(v.PortfolioId)).PortfolioCategory.Split(',').ToList());

            return View(viewModel);
        }
    }
}
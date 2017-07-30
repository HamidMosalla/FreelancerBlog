using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class HomePortfolioSlider : ViewComponent
    {
        private readonly IMapper _mapper;
        private IMediator _mediator;

        public HomePortfolioSlider(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var portfolios = await _mediator.Send(new GetAllPortfoliosQuery());

            var portfoliosViewModel = _mapper.Map<List<Portfolio>, List<PortfolioViewModel>>(portfolios.ToList());

            portfoliosViewModel.ForEach(v => v.PortfolioCategoryList = portfolios.Single(p => p.PortfolioId.Equals(v.PortfolioId)).PortfolioCategory.Split(',').ToList());

            return View(portfoliosViewModel);
        }
    }
}
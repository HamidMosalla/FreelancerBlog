using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class HomePortfolioSlider : ViewComponent
    {
        private IUnitOfWork _uw;
        private readonly IMapper _mapper;

        public HomePortfolioSlider(IUnitOfWork uw, IMapper mapper)
        {
            _uw = uw;
            _mapper = mapper;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var portfolios = await _uw.PortfolioRepository.GetAllAsync();

            var portfoliosViewModel = _mapper.Map<List<Portfolio>, List<PortfolioViewModel>>(portfolios);

            portfoliosViewModel.ForEach(v => v.PortfolioCategoryList = portfolios.Single(p => p.PortfolioId.Equals(v.PortfolioId)).PortfolioCategory.Split(',').ToList());

            return View(portfoliosViewModel);
        }

    }
}

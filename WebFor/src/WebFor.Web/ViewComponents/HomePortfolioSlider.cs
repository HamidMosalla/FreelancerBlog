using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Web.Services;
using WebFor.Web.ViewModels.Article;

namespace WebFor.Web.ViewComponents
{
    public class HomePortfolioSlider : ViewComponent
    {

        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;

        public HomePortfolioSlider(IUnitOfWork uw, IWebForMapper webForMapper)
        {
            _uw = uw;
            _webForMapper = webForMapper;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var portfolios = await _uw.PortfolioRepository.GetAllAsync();

            var portfoliosViewModel = _webForMapper.PortfolioCollectionToPortfolioCollectionViewModel(portfolios);

            return View(portfoliosViewModel);
        }

    }
}

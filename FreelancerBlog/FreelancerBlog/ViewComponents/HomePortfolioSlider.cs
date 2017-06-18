using System.Threading.Tasks;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
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

            var portfoliosViewModel = _webForMapper.PortfolioCollectionToPortfolioViewModelCollection(portfolios);

            return View(portfoliosViewModel);
        }

    }
}

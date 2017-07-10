using System.Threading.Tasks;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class HomePortfolioSlider : ViewComponent
    {

        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;

        public HomePortfolioSlider(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var portfolios = await _uw.PortfolioRepository.GetAllAsync();

            var portfoliosViewModel = _freelancerBlogMapper.PortfolioCollectionToPortfolioViewModelCollection(portfolios);

            return View(portfoliosViewModel);
        }

    }
}

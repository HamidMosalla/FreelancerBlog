using System.Threading.Tasks;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class HomeSlider : ViewComponent
    {

        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;

        public HomeSlider(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var slideShows = await _uw.SlideShowRepository.GetAllAsyncForHomePage();

            var slideShowViewModel = _freelancerBlogMapper.SlideShowCollectionToSlideShowCollectionViewModel(slideShows);

            return View(slideShowViewModel);
        }

    }
}

using System.Threading.Tasks;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class HomeSlider : ViewComponent
    {

        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;

        public HomeSlider(IUnitOfWork uw, IWebForMapper webForMapper)
        {
            _uw = uw;
            _webForMapper = webForMapper;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var slideShows = await _uw.SlideShowRepository.GetAllAsyncForHomePage();

            var slideShowViewModel = _webForMapper.SlideShowCollectionToSlideShowCollectionViewModel(slideShows);

            return View(slideShowViewModel);
        }

    }
}

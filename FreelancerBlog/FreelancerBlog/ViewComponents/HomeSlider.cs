using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.SlideShow;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class HomeSlider : ViewComponent
    {

        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;
        private readonly IMapper _mapper;

        public HomeSlider(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper, IMapper mapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
            _mapper = mapper;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var slideShows = await _uw.SlideShowRepository.GetAllAsyncForHomePage();

            var slideShowViewModel = _mapper.Map<List<SlideShow>, List<SlideShowViewModel>>(slideShows);

            return View(slideShowViewModel);
        }

    }
}

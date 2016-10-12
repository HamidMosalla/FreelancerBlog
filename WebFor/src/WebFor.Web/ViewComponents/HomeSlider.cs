using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Web.Mapper;
using WebFor.Web.ViewModels.Article;

namespace WebFor.Web.ViewComponents
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

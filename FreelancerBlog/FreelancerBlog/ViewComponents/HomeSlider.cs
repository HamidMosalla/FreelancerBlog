using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.SlideShows;
using FreelancerBlog.Web.Areas.Admin.ViewModels.SlideShow;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Web.ViewComponents
{
    public class HomeSlider : ViewComponent
    {
        private readonly IMapper _mapper;
        private IMediator _mediator;

        public HomeSlider(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var slideShows = await _mediator.Send(new GetAllSlideShowForHomePageQuery());

            var slideShowViewModel = _mapper.Map<List<SlideShow>, List<SlideShowViewModel>>(slideShows.ToList());

            return View(slideShowViewModel);
        }
    }
}
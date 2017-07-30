using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.SlideShow;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.SlideShows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
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
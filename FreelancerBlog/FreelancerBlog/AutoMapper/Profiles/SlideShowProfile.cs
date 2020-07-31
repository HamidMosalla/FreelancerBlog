using AutoMapper;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Web.Areas.Admin.ViewModels.SlideShow;

namespace FreelancerBlog.Web.AutoMapper.Profiles
{
    public class SlideShowProfile: Profile
    {
        public SlideShowProfile()
        {
            CreateMap<SlideShow, SlideShowViewModel>();
            CreateMap<SlideShowViewModel, SlideShow>();
            CreateMap<SlideShow, SlideShowViewModelEdit>();
            CreateMap<SlideShowViewModelEdit, SlideShow>();
        }
    }
}
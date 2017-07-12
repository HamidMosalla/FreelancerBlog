using AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.ViewModels.SiteOrder;

namespace FreelancerBlog.AutoMapper.Profiles
{
    public class SiteOrderProfile : Profile
    {
        public SiteOrderProfile()
        {
            CreateMap<SiteOrder, SiteOrderViewModel>();
            CreateMap<SiteOrderViewModel, SiteOrder>();
        }
    }
}
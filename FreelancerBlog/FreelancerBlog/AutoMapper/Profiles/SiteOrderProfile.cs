using AutoMapper;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Web.ViewModels.SiteOrder;

namespace FreelancerBlog.Web.AutoMapper.Profiles
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
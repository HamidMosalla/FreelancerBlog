using AutoMapper;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Web.Areas.Admin.ViewModels.Portfolio;

namespace FreelancerBlog.Web.AutoMapper.Profiles
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<Portfolio, PortfolioViewModel>();
            CreateMap<PortfolioViewModel, Portfolio>();
            CreateMap<Portfolio, PortfolioViewModelEdit>();
            CreateMap<PortfolioViewModelEdit, Portfolio>();
        }
    }
}
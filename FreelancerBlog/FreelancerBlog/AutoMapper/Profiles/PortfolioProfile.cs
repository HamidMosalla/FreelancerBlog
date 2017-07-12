using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.Core.Domain;

namespace FreelancerBlog.AutoMapper.Profiles
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
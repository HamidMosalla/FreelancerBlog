using AutoMapper;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Web.Areas.User.ViewModels.Profile;

namespace FreelancerBlog.Web.AutoMapper.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, UserProfileViewModel>();
            CreateMap<UserProfileViewModel, ApplicationUser>();
        }
    }
}
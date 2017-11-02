using AutoMapper;
using FreelancerBlog.Areas.User.ViewModels.Profile;
using FreelancerBlog.Core.DomainModels;

namespace FreelancerBlog.AutoMapper.Profiles
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
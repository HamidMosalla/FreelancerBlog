using AutoMapper;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Web.ViewModels.Contact;

namespace FreelancerBlog.Web.AutoMapper.Profiles
{
    public class ContactProfile:Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactViewModel>();
            CreateMap<ContactViewModel, Contact>();
        }
    }
}

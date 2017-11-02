using AutoMapper;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.ViewModels.Contact;

namespace FreelancerBlog.AutoMapper.Profiles
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

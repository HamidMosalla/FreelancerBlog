using AutoMapper;
using FreelancerBlog.Core.Domain;
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

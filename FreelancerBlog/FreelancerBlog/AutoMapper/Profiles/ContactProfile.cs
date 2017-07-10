using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

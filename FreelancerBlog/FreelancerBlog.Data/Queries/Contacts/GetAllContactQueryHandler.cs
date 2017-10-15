using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Contacts;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Queries.Contacts
{
    public class GetAllContactQueryHandler : IRequestHandler<GetAllContactQuery, IQueryable<Contact>>
    {
        private FreelancerBlogContext _context;

        public GetAllContactQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public IQueryable<Contact> Handle(GetAllContactQuery message)
        {
            return _context.Contacts.AsQueryable();
        }
    }
}
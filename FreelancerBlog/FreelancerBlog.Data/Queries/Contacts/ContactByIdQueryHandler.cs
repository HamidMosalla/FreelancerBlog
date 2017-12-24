using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Contacts;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.Contacts
{
    public class ContactByIdQueryHandler : AsyncRequestHandler<ContactByIdQuery, Contact>
    {
        private FreelancerBlogContext _context;

        public ContactByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task<Contact> HandleCore(ContactByIdQuery message)
        {
            return _context.Contacts.SingleOrDefaultAsync(c => c.ContactId == message.ContactId);
        }
    }
}
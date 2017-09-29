using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Contacts;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.Contacts
{
    public class ContactByIdQueryHandler : IAsyncRequestHandler<ContactByIdQuery, Contact>
    {
        private FreelancerBlogContext _context;

        public ContactByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task<Contact> Handle(ContactByIdQuery message)
        {
            return _context.Contacts.SingleOrDefaultAsync(c => c.ContactId.Equals(message.ContactId));
        }
    }
}
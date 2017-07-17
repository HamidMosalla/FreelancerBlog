using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Contacts;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Contacts
{
    public class AddNewContactCommandHandler : IAsyncRequestHandler<AddNewContactCommand>
    {
        private FreelancerBlogContext _context;

        public AddNewContactCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(AddNewContactCommand message)
        {
            _context.Contacts.Add(message.Contact);
            return _context.SaveChangesAsync();
        }
    }
}
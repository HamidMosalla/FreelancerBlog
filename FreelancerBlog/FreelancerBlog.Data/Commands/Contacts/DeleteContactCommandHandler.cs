using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Contacts;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Contacts
{
    class DeleteContactCommandHandler : IAsyncRequestHandler<DeleteContactCommand>
    {
        private FreelancerBlogContext _context;

        public DeleteContactCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteContactCommand message)
        {
            _context.Contacts.Remove(message.Contact);
           await _context.SaveChangesAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Contacts;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Contacts
{
    public class DeleteContactCommandHandler : AsyncRequestHandler<DeleteContactCommand>
    {
        private FreelancerBlogContext _context;

        public DeleteContactCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override async Task Handle(DeleteContactCommand message, CancellationToken cancellationToken)
        {
            _context.Contacts.Remove(message.Contact);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
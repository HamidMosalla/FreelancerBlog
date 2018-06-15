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
    public class AddNewContactCommandHandler : AsyncRequestHandler<AddNewContactCommand>
    {
        private FreelancerBlogContext _context;

        public AddNewContactCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override  Task Handle(AddNewContactCommand request, CancellationToken cancellationToken)
        {
            _context.Contacts.Add(request.Contact);
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
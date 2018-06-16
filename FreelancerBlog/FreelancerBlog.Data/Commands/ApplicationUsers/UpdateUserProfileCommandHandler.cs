using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.ApplicationUsers;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.ApplicationUsers
{
    public class UpdateUserProfileCommandHandler : AsyncRequestHandler<UpdateUserProfileCommand>
    {
        private FreelancerBlogContext _context;

        public UpdateUserProfileCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override  Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var model = _context.Users.AsNoTracking().Single(u => u.Id == request.ApplicationUser.Id);
            request.ApplicationUser.ConcurrencyStamp = model.ConcurrencyStamp;
            _context.Entry(request.ApplicationUser).State = EntityState.Modified;
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
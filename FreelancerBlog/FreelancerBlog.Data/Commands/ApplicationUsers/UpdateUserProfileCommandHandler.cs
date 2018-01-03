using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        protected override Task HandleCore(UpdateUserProfileCommand message)
        {
            var model = _context.Users.AsNoTracking().Single(u => u.Id == message.ApplicationUser.Id);
            message.ApplicationUser.ConcurrencyStamp = model.ConcurrencyStamp;
            _context.Entry(message.ApplicationUser).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
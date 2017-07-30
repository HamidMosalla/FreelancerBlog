using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.ApplicationUsers;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ApplicationUsers
{
    public class UserByIdQueryHandler: IAsyncRequestHandler<UserByIdQuery, ApplicationUser>
    {
        private FreelancerBlogContext _context;

        public UserByIdQueryHandler(FreelancerBlogContext contect)
        {
            _context = contect;
        }

        public Task<ApplicationUser> Handle(UserByIdQuery message)
        {
            return _context.Users.SingleAsync(u => u.Id.Equals(message.ApplicationUserId));

            //var userStore = new UserStore<ApplicationUser>(_context);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            //var user = userManager.FindById(User.Identity.GetUserId());
        }
    }
}
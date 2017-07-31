using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.ApplicationUsers;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ApplicationUsers
{
    public class UserByIdQueryHandler : IAsyncRequestHandler<UserByIdQuery, ApplicationUser>
    {
        private FreelancerBlogContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserByIdQueryHandler(FreelancerBlogContext contect, UserManager<ApplicationUser> userManager)
        {
            _context = contect;
            _userManager = userManager;
        }

        public Task<ApplicationUser> Handle(UserByIdQuery message)
        {
            return _context.Users.SingleAsync(u => u.Id.Equals(_userManager.GetUserId(message.User)));
        }
    }
}
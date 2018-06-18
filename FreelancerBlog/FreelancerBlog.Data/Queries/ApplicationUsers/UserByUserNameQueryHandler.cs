using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.ApplicationUsers;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ApplicationUsers
{
    public class UserByUserNameQueryHandler : IRequestHandler<UserByUserNameQuery, ApplicationUser>
    {
        private readonly FreelancerBlogContext _context;

        public UserByUserNameQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task<ApplicationUser> Handle(UserByUserNameQuery request, CancellationToken cancellationToken)
        {
            return _context.Users
                           .Include(u => u.Articles)
                           .ThenInclude(u => u.ArticleRatings)
                           .Include(u => u.ArticleComments)
                           .SingleOrDefaultAsync(u => u.Email == request.UserName, cancellationToken: cancellationToken);
        }
    }
}
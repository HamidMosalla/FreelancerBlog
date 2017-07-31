using System.Linq;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FreelancerBlog.Data.Queries.Articles
{
    class ArticleRatedBeforeQueryHandler : IRequestHandler<ArticleRatedBeforeQuery, bool>
    {
        private FreelancerBlogContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticleRatedBeforeQueryHandler(FreelancerBlogContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool Handle(ArticleRatedBeforeQuery message)
        {
            var ratings = _context.ArticleRatings.Where(a => a.ArticleIDfk == message.ArticleId).ToList();

            if (ratings.Count != 0)
            {
                var userId = _userManager.GetUserId(message.User);

                bool eligibility = ratings.Any(r => r.UserIDfk == userId);

                if (eligibility)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
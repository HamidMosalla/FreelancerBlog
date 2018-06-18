using System.Diagnostics;
using System.Linq;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FreelancerBlog.Data.Queries.Articles
{
    public class ArticleRatedBeforeQueryHandler : RequestHandler<ArticleRatedBeforeQuery, bool>
    {
        private readonly FreelancerBlogContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticleRatedBeforeQueryHandler(FreelancerBlogContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        protected override bool Handle(ArticleRatedBeforeQuery message)
        {
            var ratings = _context.ArticleRatings.Where(a => a.ArticleIDfk == message.ArticleId).ToList();

            if (ratings.Count == 0) return false;

            var userId = _userManager.GetUserId(message.User);

            bool userRatedBefore = ratings.Any(r => r.UserIDfk == userId);

            if (userRatedBefore)
            {
                return true;
            }

            return false;
        }
    }
}
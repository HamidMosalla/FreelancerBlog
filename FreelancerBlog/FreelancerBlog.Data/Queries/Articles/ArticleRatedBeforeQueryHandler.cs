using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Queries.Data.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Queries.Articles
{
    class ArticleRatedBeforeQueryHandler : IRequestHandler<ArticleRatedBeforeQuery, bool>
    {
        private FreelancerBlogContext _context;

        public ArticleRatedBeforeQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public bool Handle(ArticleRatedBeforeQuery message)
        {
            var ratings = _context.ArticleRatings.Where(a => a.ArticleIDfk == message.ArticleId).ToList();

            if (ratings.Count != 0)
            {
                bool eligibility = ratings.Any(r => r.UserIDfk == message.UserId);

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
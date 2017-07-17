using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class UpdateArticleRatingCommandHandler : IAsyncRequestHandler<UpdateArticleRatingCommand>
    {
        private FreelancerBlogContext _context;

        public UpdateArticleRatingCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(UpdateArticleRatingCommand message)
        {
            var articleRating = _context.ArticleRatings.Single(a => a.ArticleIDfk == message.ArticleId && a.UserIDfk == message.UserId);

            articleRating.ArticleRatingScore = message.ArticleRating;

            return _context.SaveChangesAsync();
        }
    }
}
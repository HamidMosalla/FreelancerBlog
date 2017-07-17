using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Articles;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class AddRatingToArticleCommandHandler : IAsyncRequestHandler<AddRatingToArticleCommand>
    {
        private FreelancerBlogContext _context;

        public AddRatingToArticleCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(AddRatingToArticleCommand message)
        {
            var articleRating = new ArticleRating { ArticleIDfk = message.ArticleId, ArticleRatingScore = message.ArticleRating, UserIDfk = message.UserId };

            _context.ArticleRatings.Add(articleRating);

            return _context.SaveChangesAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Articles;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class AddRatingToArticleCommandHandler : IAsyncRequestHandler<AddRatingToArticleCommand>
    {
        private FreelancerBlogContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddRatingToArticleCommandHandler(FreelancerBlogContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task Handle(AddRatingToArticleCommand message)
        {
            var articleRating = new ArticleRating { ArticleIDfk = message.ArticleId, ArticleRatingScore = message.ArticleRating, UserIDfk = _userManager.GetUserId(message.User) };

            _context.ArticleRatings.Add(articleRating);

            return _context.SaveChangesAsync();
        }
    }
}
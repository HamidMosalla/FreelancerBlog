using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Articles;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class UpdateArticleRatingCommandHandler : IAsyncRequestHandler<UpdateArticleRatingCommand>
    {
        private FreelancerBlogContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateArticleRatingCommandHandler(FreelancerBlogContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task Handle(UpdateArticleRatingCommand message)
        {
            var articleRating = _context.ArticleRatings.Single(a => a.ArticleIDfk == message.ArticleId && a.UserIDfk == _userManager.GetUserId(message.User));

            articleRating.ArticleRatingScore = message.ArticleRating;

            return _context.SaveChangesAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Articles;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class AddRatingToArticleCommandHandler : AsyncRequestHandler<AddRatingToArticleCommand>
    {
        private FreelancerBlogContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddRatingToArticleCommandHandler(FreelancerBlogContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        protected override  Task Handle(AddRatingToArticleCommand request, CancellationToken cancellationToken)
        {
            var articleRating = new ArticleRating { ArticleIDfk = request.ArticleId, ArticleRatingScore = request.ArticleRating, UserIDfk = _userManager.GetUserId(request.User) };

            _context.ArticleRatings.Add(articleRating);

            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
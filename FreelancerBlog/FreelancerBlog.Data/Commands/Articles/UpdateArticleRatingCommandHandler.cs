using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UpdateArticleRatingCommandHandler : AsyncRequestHandler<UpdateArticleRatingCommand>
    {
        private FreelancerBlogContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateArticleRatingCommandHandler(FreelancerBlogContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        protected override  Task Handle(UpdateArticleRatingCommand request, CancellationToken cancellationToken)
        {
            var articleRating = _context.ArticleRatings.Single(a =>
                a.ArticleIDfk == request.ArticleId &&
                a.UserIDfk == _userManager.GetUserId(request.User));

            articleRating.ArticleRatingScore = request.ArticleRating;

            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
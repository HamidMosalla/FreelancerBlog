using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Articles;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FreelancerBlog.Data.Commands.Articles
{
    class CreateArticleCommandHandler : IAsyncRequestHandler<CreateArticleCommand>
    {
        private FreelancerBlogContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateArticleCommandHandler(FreelancerBlogContext context, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public Task Handle(CreateArticleCommand message)
        {
            message.Article.ArticleDateCreated = DateTime.Now;
            message.Article.ArticleViewCount = 1;
            message.Article.UserIDfk = _userManager.GetUserId(_contextAccessor.HttpContext.User);

            _context.Articles.Add(message.Article);

           return _context.SaveChangesAsync();
        }
    }
}
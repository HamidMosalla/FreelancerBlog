using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class UpdateArticleCommandHandler : IAsyncRequestHandler<UpdateArticleCommand>
    {
        private readonly FreelancerBlogContext _context;

        public UpdateArticleCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(UpdateArticleCommand message)
        {
            _context.Articles.Attach(message.Article);

            message.Article.ArticleDateModified = DateTime.Now;

            var entity = _context.Entry(message.Article);
            entity.State = EntityState.Modified;

            entity.Property(e => e.ArticleDateCreated).IsModified = false;
            entity.Property(e => e.ArticleId).IsModified = false;
            entity.Property(e => e.ArticleViewCount).IsModified = false;
            entity.Property(e => e.UserIDfk).IsModified = false;

            return _context.SaveChangesAsync();
        }
    }
}
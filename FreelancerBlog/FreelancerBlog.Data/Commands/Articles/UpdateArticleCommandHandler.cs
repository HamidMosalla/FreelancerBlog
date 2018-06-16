using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class UpdateArticleCommandHandler : AsyncRequestHandler<UpdateArticleCommand>
    {
        private readonly FreelancerBlogContext _context;

        public UpdateArticleCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override  Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            _context.Articles.Attach(request.Article);

            request.Article.ArticleDateModified = DateTime.Now;

            var entity = _context.Entry(request.Article);
            entity.State = EntityState.Modified;

            entity.Property(e => e.ArticleDateCreated).IsModified = false;
            entity.Property(e => e.ArticleId).IsModified = false;
            entity.Property(e => e.ArticleViewCount).IsModified = false;
            entity.Property(e => e.UserIDfk).IsModified = false;

            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
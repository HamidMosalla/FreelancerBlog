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
    public class IncreaseArticleViewCountCommandHandler : AsyncRequestHandler<IncreaseArticleViewCountCommand>
    {
        private FreelancerBlogContext _context;

        public IncreaseArticleViewCountCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override async Task Handle(IncreaseArticleViewCountCommand request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.SingleAsync(a => a.ArticleId == request.ArticleId);

            article.ArticleViewCount += 1;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
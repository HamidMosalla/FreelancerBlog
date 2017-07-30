using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Articles;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class IncreaseArticleViewCountCommandHandler : IAsyncRequestHandler<IncreaseArticleViewCountCommand>
    {
        private FreelancerBlogContext _context;

        public IncreaseArticleViewCountCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public async Task Handle(IncreaseArticleViewCountCommand message)
        {
            var article = await _context.Articles.SingleAsync(a => a.ArticleId == message.ArticleId);

            article.ArticleViewCount += 1;

            await _context.SaveChangesAsync();
        }
    }
}
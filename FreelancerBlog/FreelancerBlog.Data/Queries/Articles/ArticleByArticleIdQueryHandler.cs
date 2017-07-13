using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Article;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.Articles
{
    class ArticleByArticleIdQueryHandler : IAsyncRequestHandler<ArticleByArticleIdQuery, Article>
    {
        private readonly FreelancerBlogContext _context;

        public ArticleByArticleIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public async Task<Article> Handle(ArticleByArticleIdQuery message)
        {
            return await _context.Articles.SingleAsync(a => a.ArticleId == message.ArticleId);
        }
    }
}
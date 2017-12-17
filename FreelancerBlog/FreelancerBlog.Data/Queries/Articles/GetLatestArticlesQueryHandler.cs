using System.Linq;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Queries.Articles
{
    public class GetLatestArticlesQueryHandler : RequestHandler<GetLatestArticlesQuery, IQueryable<Article>>
    {
        private FreelancerBlogContext _context;

        public GetLatestArticlesQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override IQueryable<Article> HandleCore(GetLatestArticlesQuery message)
        {
            return _context.Articles.OrderByDescending(a => a.ArticleDateCreated).Take(message.NumberOfArticles).AsQueryable();
        }
    }
}
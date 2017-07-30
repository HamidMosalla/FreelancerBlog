using System.Linq;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Queries.Articles
{
    public class GetLatestArticlesQueryHandler : IRequestHandler<GetLatestArticlesQuery, IQueryable<Article>>
    {
        private FreelancerBlogContext _context;

        public GetLatestArticlesQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public IQueryable<Article> Handle(GetLatestArticlesQuery message)
        {
            return _context.Articles.OrderByDescending(a => a.ArticleDateCreated).Take(message.NumberOfArticles).AsQueryable();
        }
    }
}
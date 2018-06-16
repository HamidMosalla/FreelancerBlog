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

        protected override IQueryable<Article> Handle(GetLatestArticlesQuery request)
        {
            return _context.Articles.OrderByDescending(a => a.ArticleDateCreated).Take(request.NumberOfArticles).AsQueryable();
        }
    }
}
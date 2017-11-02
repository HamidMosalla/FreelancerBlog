using System.Linq;
using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Articles
{
    public class GetLatestArticlesQuery : IRequest<IQueryable<Article>>
    {
        public int NumberOfArticles { get; set; }
    }
}
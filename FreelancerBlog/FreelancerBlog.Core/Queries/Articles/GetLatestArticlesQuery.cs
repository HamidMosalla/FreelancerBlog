using System.Linq;
using MediatR;

namespace FreelancerBlog.Core.Queries.Articles
{
    public class GetLatestArticlesQuery : IRequest<IQueryable<Domain.Article>>
    {
        public int NumberOfArticles { get; set; }
    }
}
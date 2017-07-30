using System.Linq;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Articles
{
    public class GetLatestArticlesQuery : IRequest<IQueryable<Domain.Article>>
    {
        public int NumberOfArticles { get; set; }
    }
}
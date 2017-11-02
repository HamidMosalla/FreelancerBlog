using System.Linq;
using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Articles
{
    public class ArticlesByTagQuery : IRequest<IQueryable<Article>>
    {
        public int TagId { get; set; }
    }
}

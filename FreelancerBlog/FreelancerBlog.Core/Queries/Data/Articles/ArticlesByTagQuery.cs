using System.Linq;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Articles
{
    public class ArticlesByTagQuery : IRequest<IQueryable<Domain.Article>>
    {
        public int TagId { get; set; }
    }
}

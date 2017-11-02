using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Articles
{
    public class ArticleByArticleIdQuery : IRequest<Article>
    {
        public int ArticleId { get; set; }
    }
}
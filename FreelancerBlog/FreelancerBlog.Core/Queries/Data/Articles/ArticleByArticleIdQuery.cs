using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Articles
{
    public class ArticleByArticleIdQuery : IRequest<Domain.Article>
    {
        public int ArticleId { get; set; }
    }
}
using MediatR;

namespace FreelancerBlog.Core.Queries.Article
{
    public class ArticleByArticleIdQuery : IRequest<Domain.Article>
    {
        public int ArticleId { get; set; }
    }
}
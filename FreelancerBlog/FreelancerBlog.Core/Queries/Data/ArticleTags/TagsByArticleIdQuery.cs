using MediatR;

namespace FreelancerBlog.Core.Queries.Data.ArticleTags
{
    public class TagsByArticleIdQuery : IRequest<string>
    {
        public int ArticleId { get; set; }
    }
}
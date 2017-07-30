using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.ArticleTags
{
    public class FindArticleTagByIdQuery : IRequest<ArticleTag>
    {
        public int ArticleTagId { get; set; }
    }
}
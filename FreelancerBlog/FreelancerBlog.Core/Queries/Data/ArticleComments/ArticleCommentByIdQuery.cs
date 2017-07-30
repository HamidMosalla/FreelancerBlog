using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.ArticleComments
{
    public class ArticleCommentByIdQuery : IRequest<ArticleComment>
    {
        public int ArticleCommentId { get; set; }
    }
}
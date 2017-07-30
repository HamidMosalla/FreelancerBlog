using MediatR;

namespace FreelancerBlog.Core.Commands.Data.Articles
{
    public class IncreaseArticleViewCountCommand : IRequest
    {
        public int ArticleId { get; set; }
    }
}
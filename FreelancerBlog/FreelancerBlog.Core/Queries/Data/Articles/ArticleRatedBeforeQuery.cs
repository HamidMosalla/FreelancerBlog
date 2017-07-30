using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Articles
{
    public class ArticleRatedBeforeQuery : IRequest<bool>
    {
        public int ArticleId { get; set; }
        public string UserId { get; set; }
    }
}
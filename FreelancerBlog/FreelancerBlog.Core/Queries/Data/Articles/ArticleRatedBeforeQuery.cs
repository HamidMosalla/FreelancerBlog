using System.Security.Claims;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Articles
{
    public class ArticleRatedBeforeQuery : IRequest<bool>
    {
        public int ArticleId { get; set; }
        public ClaimsPrincipal User { get; set; }
    }
}
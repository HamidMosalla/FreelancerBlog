using System.Security.Claims;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.Articles
{
    public class UpdateArticleRatingCommand : IRequest
    {
        public int ArticleId { get; set; }
        public double ArticleRating { get; set; }
        public ClaimsPrincipal User { get; set; }
    }
}
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.Articles
{
    public class AddRatingToArticleCommand : IRequest
    {
        public int ArticleId { get; set; }
        public double ArticleRating { get; set; }
        public string UserId { get; set; }
    }
}
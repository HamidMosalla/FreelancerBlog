using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.Articles
{
    public class DeleteArticleCommand : IRequest
    {
        public Article Article { get; set; }
    }
}
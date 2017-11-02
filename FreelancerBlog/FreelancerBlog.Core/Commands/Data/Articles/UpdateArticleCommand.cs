using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.Articles
{
    public class UpdateArticleCommand : IRequest
    {
        public Article Article { get; set; }
    }
}
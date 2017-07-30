using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.ArticleTags
{
    public class DeleteArticleTagCommand : IRequest
    {
        public ArticleTag ArticleTag { get; set; }
    }
}
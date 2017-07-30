using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.ArticleComments
{
    public class AddCommentToArticleCommand : IRequest
    {
        public ArticleComment ArticleComment { get; set; }
    }
}
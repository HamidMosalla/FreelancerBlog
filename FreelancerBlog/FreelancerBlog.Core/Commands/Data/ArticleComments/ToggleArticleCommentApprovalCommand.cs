using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.ArticleComments
{
    public class ToggleArticleCommentApprovalCommand : IRequest
    {
        public ArticleComment ArticleComment { get; set; }
    }
}
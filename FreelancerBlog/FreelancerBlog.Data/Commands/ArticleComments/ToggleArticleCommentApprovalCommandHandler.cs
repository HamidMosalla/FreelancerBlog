using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.ArticleComments;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.ArticleComments
{
    class ToggleArticleCommentApprovalCommandHandler : IAsyncRequestHandler <ToggleArticleCommentApprovalCommand>
    {
        private FreelancerBlogContext _context;

        public ToggleArticleCommentApprovalCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(ToggleArticleCommentApprovalCommand message)
        {
            message.ArticleComment.IsCommentApproved = !message.ArticleComment.IsCommentApproved;

            return _context.SaveChangesAsync();
        }
    }
}
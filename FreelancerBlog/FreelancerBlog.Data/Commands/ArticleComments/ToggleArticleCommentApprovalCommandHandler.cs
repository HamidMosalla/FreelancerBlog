using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.ArticleComments;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.ArticleComments
{
    class ToggleArticleCommentApprovalCommandHandler : AsyncRequestHandler <ToggleArticleCommentApprovalCommand>
    {
        private FreelancerBlogContext _context;

        public ToggleArticleCommentApprovalCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task HandleCore(ToggleArticleCommentApprovalCommand message)
        {
            message.ArticleComment.IsCommentApproved = !message.ArticleComment.IsCommentApproved;

            return _context.SaveChangesAsync();
        }
    }
}
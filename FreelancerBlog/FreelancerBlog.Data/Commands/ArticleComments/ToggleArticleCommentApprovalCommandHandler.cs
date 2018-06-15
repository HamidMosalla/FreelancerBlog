using System.Threading;
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

        protected override  Task Handle(ToggleArticleCommentApprovalCommand request, CancellationToken cancellationToken)
        {
            request.ArticleComment.IsCommentApproved = !request.ArticleComment.IsCommentApproved;

            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
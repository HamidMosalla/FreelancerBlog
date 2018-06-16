using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.ArticleComments;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.ArticleComments
{
    class EditArticleCommentCommandHandler : AsyncRequestHandler<EditArticleCommentCommand>
    {
        private FreelancerBlogContext _context;

        public EditArticleCommentCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override  Task Handle(EditArticleCommentCommand request, CancellationToken cancellationToken)
        {
            request.ArticleComment.ArticleCommentBody = request.NewCommentBody;
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
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
    class DeleteArticleCommentCommandHandler : AsyncRequestHandler<DeleteArticleCommentCommand>
    {
        private FreelancerBlogContext _context;

        public DeleteArticleCommentCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task Handle(DeleteArticleCommentCommand request, CancellationToken cancellationToken)
        {
            _context.ArticleComments.Remove(request.ArticleComment);
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
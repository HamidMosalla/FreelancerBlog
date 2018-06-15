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
    class AddCommentToArticleCommandHandler : AsyncRequestHandler<AddCommentToArticleCommand>
    {
        private FreelancerBlogContext _context;

        public AddCommentToArticleCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override  Task Handle(AddCommentToArticleCommand request, CancellationToken cancellationToken)
        {
            _context.ArticleComments.Add(request.ArticleComment);

            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
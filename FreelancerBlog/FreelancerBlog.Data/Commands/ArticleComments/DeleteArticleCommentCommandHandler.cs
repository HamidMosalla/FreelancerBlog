using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.ArticleComments;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.ArticleComments
{
    class DeleteArticleCommentCommandHandler : IAsyncRequestHandler<DeleteArticleCommentCommand>
    {
        private FreelancerBlogContext _context;

        public DeleteArticleCommentCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(DeleteArticleCommentCommand message)
        {
            _context.ArticleComments.Remove(message.ArticleComment);
            return _context.SaveChangesAsync();
        }
    }
}
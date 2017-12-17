using System;
using System.Collections.Generic;
using System.Text;
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

        protected override Task HandleCore(DeleteArticleCommentCommand message)
        {
            _context.ArticleComments.Remove(message.ArticleComment);
            return _context.SaveChangesAsync();
        }
    }
}
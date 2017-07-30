using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.ArticleComments;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.ArticleComments
{
    class EditArticleCommentCommandHandler : IAsyncRequestHandler<EditArticleCommentCommand>
    {
        private FreelancerBlogContext _context;

        public EditArticleCommentCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(EditArticleCommentCommand message)
        {
            message.ArticleComment.ArticleCommentBody = message.NewCommentBody;
            return _context.SaveChangesAsync();
        }
    }
}
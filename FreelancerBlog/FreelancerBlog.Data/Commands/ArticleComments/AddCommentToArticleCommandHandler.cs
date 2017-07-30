using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.ArticleComments;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.ArticleComments
{
    class AddCommentToArticleCommandHandler : IAsyncRequestHandler<AddCommentToArticleCommand>
    {
        private FreelancerBlogContext _context;

        public AddCommentToArticleCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(AddCommentToArticleCommand message)
        {
            _context.ArticleComments.Add(message.ArticleComment);

            return _context.SaveChangesAsync();
        }
    }
}
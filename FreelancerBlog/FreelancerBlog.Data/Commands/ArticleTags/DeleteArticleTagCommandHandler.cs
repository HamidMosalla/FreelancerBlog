using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.ArticleTags
{
    public class DeleteArticleTagCommandHandler : IAsyncRequestHandler<DeleteArticleTagCommand>
    {
        private readonly FreelancerBlogContext _context;

        public DeleteArticleTagCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(DeleteArticleTagCommand message)
        {
            _context.ArticleTags.Remove(message.ArticleTag);

            return _context.SaveChangesAsync();
        }
    }
}
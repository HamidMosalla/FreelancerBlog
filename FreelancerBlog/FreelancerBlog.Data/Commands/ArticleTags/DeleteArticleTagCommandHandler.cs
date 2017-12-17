using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.ArticleTags
{
    public class DeleteArticleTagCommandHandler : AsyncRequestHandler<DeleteArticleTagCommand>
    {
        private readonly FreelancerBlogContext _context;

        public DeleteArticleTagCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task HandleCore(DeleteArticleTagCommand message)
        {
            _context.ArticleTags.Remove(message.ArticleTag);

            return _context.SaveChangesAsync();
        }
    }
}
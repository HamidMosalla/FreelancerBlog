using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class DeleteArticleCommandHandler : IAsyncRequestHandler<DeleteArticleCommand>
    {
        private FreelancerBlogContext _context;

        public DeleteArticleCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }


        public async Task Handle(DeleteArticleCommand message)
        {
            _context.Articles.Remove(message.Article);
            await _context.SaveChangesAsync();
        }
    }
}
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Articles
{
    public class DeleteArticleCommandHandler : AsyncRequestHandler<DeleteArticleCommand>
    {
        private FreelancerBlogContext _context;

        public DeleteArticleCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }


        protected override async Task HandleCore(DeleteArticleCommand message)
        {
            _context.Articles.Remove(message.Article);
            await _context.SaveChangesAsync();
        }
    }
}
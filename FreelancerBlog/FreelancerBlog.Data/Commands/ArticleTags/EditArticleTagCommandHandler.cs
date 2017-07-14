using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.ArticleTags
{
    class EditArticleTagCommandHandler : IAsyncRequestHandler<EditArticleTagCommand>
    {
        private readonly FreelancerBlogContext _context;

        public EditArticleTagCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(EditArticleTagCommand message)
        {
            message.ArticleTag.ArticleTagName = message.NewTagName;
            return _context.SaveChangesAsync();
        }
    }
}
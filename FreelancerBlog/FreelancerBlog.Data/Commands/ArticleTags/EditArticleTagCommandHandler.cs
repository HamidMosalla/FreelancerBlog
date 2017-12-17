using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.ArticleTags
{
    class EditArticleTagCommandHandler : AsyncRequestHandler<EditArticleTagCommand>
    {
        private readonly FreelancerBlogContext _context;

        public EditArticleTagCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task HandleCore(EditArticleTagCommand message)
        {
            message.ArticleTag.ArticleTagName = message.NewTagName;
            return _context.SaveChangesAsync();
        }
    }
}
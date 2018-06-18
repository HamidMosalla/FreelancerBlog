using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Queries.Data.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleTags
{
    class GetAllTagNamesQueryHandler : IRequestHandler<GetAllTagNamesQuery, string[]>
    {
        private FreelancerBlogContext _context;
        public GetAllTagNamesQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public async Task<string[]> Handle(GetAllTagNamesQuery request, CancellationToken cancellationToken)
        {
            return await _context.ArticleTags.Select(a => a.ArticleTagName).ToArrayAsync(cancellationToken: cancellationToken);
        }
    }
}
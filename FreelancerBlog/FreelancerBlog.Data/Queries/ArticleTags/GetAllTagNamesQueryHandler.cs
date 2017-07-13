using System.Linq;
using System.Threading.Tasks;
using FreelancerBlog.Core.Queries.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleTags
{
    class GetAllTagNamesQueryHandler : IAsyncRequestHandler<GetAllTagNamesQuery, string[]>
    {
        private FreelancerBlogContext _context;
        public GetAllTagNamesQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public async Task<string[]> Handle(GetAllTagNamesQuery message)
        {
            return await _context.ArticleTags.Select(a => a.ArticleTagName).ToArrayAsync();
        }
    }
}
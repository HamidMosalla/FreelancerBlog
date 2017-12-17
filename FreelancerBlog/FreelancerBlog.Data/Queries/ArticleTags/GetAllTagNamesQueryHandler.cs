using System.Linq;
using System.Threading.Tasks;
using FreelancerBlog.Core.Queries.Data.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleTags
{
    class GetAllTagNamesQueryHandler : AsyncRequestHandler<GetAllTagNamesQuery, string[]>
    {
        private FreelancerBlogContext _context;
        public GetAllTagNamesQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override async Task<string[]> HandleCore(GetAllTagNamesQuery request)
        {
            return await _context.ArticleTags.Select(a => a.ArticleTagName).ToArrayAsync();
        }
    }
}
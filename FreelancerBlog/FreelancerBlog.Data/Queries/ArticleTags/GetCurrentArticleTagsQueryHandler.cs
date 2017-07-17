using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleTags
{
    class GetCurrentArticleTagsQueryHandler : IAsyncRequestHandler<GetCurrentArticleTagsQuery, List<ArticleTag>>
    {
        private FreelancerBlogContext _context;

        public GetCurrentArticleTagsQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task<List<ArticleTag>> Handle(GetCurrentArticleTagsQuery message)
        {
            return _context.ArticleArticleTags
                        .Where(a => a.ArticleId == message.ArticleId)
                        .Join(_context.ArticleTags.ToList(), aat => aat.ArticleTagId, at => at.ArticleTagId, (aat, at) => at)
                        .ToListAsync();
        }
    }
}
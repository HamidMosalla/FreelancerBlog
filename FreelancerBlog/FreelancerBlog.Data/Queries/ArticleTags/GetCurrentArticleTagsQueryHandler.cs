using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleTags
{
    public class GetCurrentArticleTagsQueryHandler : AsyncRequestHandler<GetCurrentArticleTagsQuery, List<ArticleTag>>
    {
        private readonly FreelancerBlogContext _context;

        public GetCurrentArticleTagsQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task<List<ArticleTag>> HandleCore(GetCurrentArticleTagsQuery message)
        {
            return _context.ArticleArticleTags
                        .Where(a => a.ArticleId == message.ArticleId)
                        .Join(_context.ArticleTags.ToList(), aat => aat.ArticleTagId, at => at.ArticleTagId, (aat, at) => at)
                        .ToListAsync();
        }
    }
}
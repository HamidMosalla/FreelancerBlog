using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Queries.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleTags
{
    class TagsByArticleIdQueryHandler : IAsyncRequestHandler<TagsByArticleIdQuery, string>
    {
        private FreelancerBlogContext _context;

        public TagsByArticleIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(TagsByArticleIdQuery message)
        {
            var listOfArticleTags = await _context.ArticleArticleTags.Where(a => a.ArticleId == message.ArticleId).ToListAsync();

            var arrayOfTags = listOfArticleTags.Join(await _context.ArticleTags.ToListAsync(), a => a.ArticleTagId, t => t.ArticleTagId, (a, t) => t.ArticleTagName).ToArray();

            return string.Join(",", arrayOfTags);
        }
    }
}
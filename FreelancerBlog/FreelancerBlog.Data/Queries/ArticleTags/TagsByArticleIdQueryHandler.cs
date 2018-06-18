using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Queries.Data.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleTags
{
    public class TagsByArticleIdQueryHandler : IRequestHandler<TagsByArticleIdQuery, string>
    {
        private readonly FreelancerBlogContext _context;

        public TagsByArticleIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(TagsByArticleIdQuery request, CancellationToken cancellationToken)
        {
            var listOfArticleTags = await _context.ArticleArticleTags.Where(a => a.ArticleId == request.ArticleId).ToListAsync(cancellationToken: cancellationToken);

            var arrayOfTags = listOfArticleTags.Join(await _context.ArticleTags.ToListAsync(cancellationToken: cancellationToken), a => a.ArticleTagId, t => t.ArticleTagId, (a, t) => t.ArticleTagName).ToArray();

            return string.Join(",", arrayOfTags);
        }
    }
}
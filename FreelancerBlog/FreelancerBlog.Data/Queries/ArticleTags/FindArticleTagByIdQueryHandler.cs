using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleTags
{
    class FindArticleTagByIdQueryHandler : IRequestHandler<FindArticleTagByIdQuery, ArticleTag>
    {
        private FreelancerBlogContext _context;

        public FindArticleTagByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task<ArticleTag> Handle(FindArticleTagByIdQuery request, CancellationToken cancellationToken)
        {
            return _context.ArticleTags.SingleOrDefaultAsync(a => a.ArticleTagId == request.ArticleTagId, cancellationToken: cancellationToken);
        }
    }
}
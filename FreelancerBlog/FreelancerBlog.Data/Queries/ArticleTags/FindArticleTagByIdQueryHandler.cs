using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleTags
{
    class FindArticleTagByIdQueryHandler: IAsyncRequestHandler<FindArticleTagByIdQuery, ArticleTag>
    {
        private FreelancerBlogContext _context;

        public FindArticleTagByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task<ArticleTag> Handle(FindArticleTagByIdQuery message)
        {
            return _context.ArticleTags.SingleOrDefaultAsync(a => a.ArticleTagId == message.ArticleTagId);
        }
    }
}
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
    class GetAllArticleTagsQueryHandler : RequestHandler<GetAllArticleTagsQuery, IQueryable<ArticleTag>>
    {
        private FreelancerBlogContext _context;

        public GetAllArticleTagsQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override IQueryable<ArticleTag> HandleCore(GetAllArticleTagsQuery message)
        {
            return _context.ArticleTags.AsQueryable();
        }
    }
}
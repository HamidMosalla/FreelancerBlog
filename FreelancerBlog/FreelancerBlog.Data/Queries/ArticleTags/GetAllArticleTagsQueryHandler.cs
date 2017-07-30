using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.ArticleTags;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleTags
{
    class GetAllArticleTagsQueryHandler : IRequestHandler<GetAllArticleTagsQuery, IQueryable<ArticleTag>>
    {
        private FreelancerBlogContext _context;

        public GetAllArticleTagsQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public IQueryable<ArticleTag> Handle(GetAllArticleTagsQuery message)
        {
            return _context.ArticleTags.AsQueryable();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.Articles
{
    public class ArticlesByTagQueryHandler : IRequestHandler<ArticlesByTagQuery, IQueryable<Article>>
    {
        private FreelancerBlogContext _context;

        public ArticlesByTagQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public IQueryable<Article> Handle(ArticlesByTagQuery message)
        {
            return _context.ArticleArticleTags.Where(a => a.ArticleTagId.Equals(message.TagId))
                    .Join(_context.Articles.Include(a => a.ApplicationUser)
                                           .Include(a => a.ArticleComments)
                                           .Include(a => a.ArticleRatings), left => left.ArticleId, right => right.ArticleId, (aat, a) => a)
                                           .AsQueryable();
        }
    }
}
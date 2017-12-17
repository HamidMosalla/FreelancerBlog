using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.ArticleComments;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleComments
{
    class ArticleCommentByIdQueryHandler : AsyncRequestHandler<ArticleCommentByIdQuery, ArticleComment>
    {
        private FreelancerBlogContext _context;

        public ArticleCommentByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task<ArticleComment> HandleCore(ArticleCommentByIdQuery message)
        {
            return _context.ArticleComments.SingleOrDefaultAsync(a => a.ArticleCommentId == message.ArticleCommentId);
        }
    }
}

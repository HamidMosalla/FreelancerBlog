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
    class ArticleCommentByIdQueryHandler : IAsyncRequestHandler<ArticleCommentByIdQuery, ArticleComment>
    {
        private FreelancerBlogContext _context;

        public ArticleCommentByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task<ArticleComment> Handle(ArticleCommentByIdQuery message)
        {
            return _context.ArticleComments.SingleOrDefaultAsync(a => a.ArticleCommentId == message.ArticleCommentId);
        }
    }
}

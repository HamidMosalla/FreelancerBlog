using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.ArticleComments;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.ArticleComments
{
    class ArticleCommentByIdQueryHandler : IRequestHandler<ArticleCommentByIdQuery, ArticleComment>
    {
        private readonly FreelancerBlogContext _context;

        public ArticleCommentByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public  Task<ArticleComment> Handle(ArticleCommentByIdQuery request, CancellationToken cancellationToken)
        {
            return _context.ArticleComments.SingleOrDefaultAsync(a => a.ArticleCommentId == request.ArticleCommentId, cancellationToken: cancellationToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Articles;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.Articles
{
    public class GetAriclesQueryHandler : IRequestHandler<GetAriclesQuery, IQueryable<Article>>
    {
        private FreelancerBlogContext _context;

        public GetAriclesQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public IQueryable<Article> Handle(GetAriclesQuery message)
        {
            return _context.Articles;
        }
    }
}

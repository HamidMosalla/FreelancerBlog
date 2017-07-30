using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Queries.Portfolios
{
    public class GetAllPortfoliosQueryHandler : IRequestHandler<GetAllPortfoliosQuery, IQueryable<Portfolio>>
    {
        private FreelancerBlogContext _context;

        public GetAllPortfoliosQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public IQueryable<Portfolio> Handle(GetAllPortfoliosQuery message)
        {
            return _context.Portfolios.AsQueryable();
        }
    }
}

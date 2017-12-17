using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Queries.Portfolios
{
    public class GetAllPortfoliosQueryHandler : RequestHandler<GetAllPortfoliosQuery, IQueryable<Portfolio>>
    {
        private FreelancerBlogContext _context;

        public GetAllPortfoliosQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override IQueryable<Portfolio> HandleCore(GetAllPortfoliosQuery message)
        {
            return _context.Portfolios.AsQueryable();
        }
    }
}

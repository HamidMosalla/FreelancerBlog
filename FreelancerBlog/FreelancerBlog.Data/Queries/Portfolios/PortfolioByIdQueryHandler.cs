using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.Portfolios
{
    public class PortfolioByIdQueryHandler: AsyncRequestHandler<PortfolioByIdQuery, Portfolio>
    {
        private FreelancerBlogContext _context;

        public PortfolioByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task<Portfolio> HandleCore(PortfolioByIdQuery message)
        {
            return _context.Portfolios.SingleAsync(p => p.PortfolioId == message.PortfolioId);
        }
    }
}

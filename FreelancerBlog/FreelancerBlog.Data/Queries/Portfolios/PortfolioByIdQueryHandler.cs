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
    class PortfolioByIdQueryHandler: IAsyncRequestHandler<PortfolioByIdQuery, Portfolio>
    {
        private FreelancerBlogContext _context;

        public PortfolioByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task<Portfolio> Handle(PortfolioByIdQuery message)
        {
            return _context.Portfolios.SingleAsync(p => p.PortfolioId == message.PortfolioId);
        }
    }
}

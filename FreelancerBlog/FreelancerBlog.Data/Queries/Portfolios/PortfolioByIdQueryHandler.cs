using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.Portfolios
{
    public class PortfolioByIdQueryHandler : IRequestHandler<PortfolioByIdQuery, Portfolio>
    {
        private FreelancerBlogContext _context;

        public PortfolioByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task<Portfolio> Handle(PortfolioByIdQuery message, CancellationToken cancellationToken)
        {
            return _context.Portfolios.SingleAsync(p => p.PortfolioId == message.PortfolioId, cancellationToken: cancellationToken);
        }
    }
}

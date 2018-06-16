using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.Portfolios
{
    public class UpdatePortfolioCommandHandler: AsyncRequestHandler<UpdatePortfolioCommand>
    {
        private FreelancerBlogContext _context;

        public UpdatePortfolioCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override  Task Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
        {
            _context.Portfolios.Attach(request.Portfolio);

            var entity = _context.Entry(request.Portfolio);
            entity.State = EntityState.Modified;

            entity.Property(e => e.PortfolioDateCreated).IsModified = false;
            entity.Property(e => e.PortfolioThumbnail).IsModified = request.Portfolio.PortfolioThumbnail != null;

            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}

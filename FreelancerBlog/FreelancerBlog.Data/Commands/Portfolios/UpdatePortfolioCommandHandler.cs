using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.Portfolios
{
    public class UpdatePortfolioCommandHandler: IAsyncRequestHandler<UpdatePortfolioCommand>
    {
        private FreelancerBlogContext _context;

        public UpdatePortfolioCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(UpdatePortfolioCommand message)
        {
            _context.Portfolios.Attach(message.Portfolio);

            var entity = _context.Entry(message.Portfolio);
            entity.State = EntityState.Modified;

            entity.Property(e => e.PortfolioDateCreated).IsModified = false;
            entity.Property(e => e.PortfolioThumbnail).IsModified = message.Portfolio.PortfolioThumbnail != null;

            return _context.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Portfolios
{
    class DeletePortfolioCommandHandler : IAsyncRequestHandler<DeletePortfolioCommand>
    {
        private FreelancerBlogContext _context;

        public DeletePortfolioCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(DeletePortfolioCommand message)
        {
            _context.Portfolios.Remove(message.Portfolio);
            return _context.SaveChangesAsync();
        }
    }
}

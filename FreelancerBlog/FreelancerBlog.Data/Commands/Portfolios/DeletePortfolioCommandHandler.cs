using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Portfolios
{
    class DeletePortfolioCommandHandler : AsyncRequestHandler<DeletePortfolioCommand>
    {
        private FreelancerBlogContext _context;

        public DeletePortfolioCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task HandleCore(DeletePortfolioCommand message)
        {
            _context.Portfolios.Remove(message.Portfolio);
            return _context.SaveChangesAsync();
        }
    }
}

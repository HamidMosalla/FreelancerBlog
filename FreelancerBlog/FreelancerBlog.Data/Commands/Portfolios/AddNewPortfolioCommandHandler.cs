using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Portfolios
{
    public class AddNewPortfolioCommandHandler : IAsyncRequestHandler<AddNewPortfolioCommand>
    {
        private readonly FreelancerBlogContext _context;

        public AddNewPortfolioCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(AddNewPortfolioCommand message)
        {
            _context.Portfolios.Add(message.Portfolio);
            return _context.SaveChangesAsync();
        }
    }
}
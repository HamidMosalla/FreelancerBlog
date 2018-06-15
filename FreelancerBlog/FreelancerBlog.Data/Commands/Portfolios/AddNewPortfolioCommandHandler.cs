using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.Portfolios;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.Portfolios
{
    public class AddNewPortfolioCommandHandler : AsyncRequestHandler<AddNewPortfolioCommand>
    {
        private readonly FreelancerBlogContext _context;

        public AddNewPortfolioCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override  Task Handle(AddNewPortfolioCommand request, CancellationToken cancellationToken)
        {
            _context.Portfolios.Add(request.Portfolio);
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
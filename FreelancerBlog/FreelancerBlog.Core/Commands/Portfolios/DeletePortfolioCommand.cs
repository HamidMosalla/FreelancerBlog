using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Portfolios
{
    public class DeletePortfolioCommand:IRequest
    {
        public Portfolio Portfolio { get; set; }
    }
}

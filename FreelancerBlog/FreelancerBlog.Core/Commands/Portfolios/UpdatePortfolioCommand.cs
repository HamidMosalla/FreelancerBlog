using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Portfolios
{
    public class UpdatePortfolioCommand : IRequest
    {
        public Portfolio Portfolio { get; set; }
    }
}
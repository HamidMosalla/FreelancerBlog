using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace FreelancerBlog.Features.Commands
{
    public class PopulatePortfolioCategoryListCommandHandler : IRequestHandler<PopulatePortfolioCategoryListCommand>
    {
        public void Handle(PopulatePortfolioCategoryListCommand message)
        {
            message.ViewModel.ForEach(v => v.PortfolioCategoryList = message.Portfolios.Single(p => p.PortfolioId == v.PortfolioId).PortfolioCategory.Split(',').ToList());
        }
    }
}
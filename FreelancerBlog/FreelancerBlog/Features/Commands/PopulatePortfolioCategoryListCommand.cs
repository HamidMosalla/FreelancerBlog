using System.Collections.Generic;
using System.Linq;
using FreelancerBlog.Areas.Admin.ViewModels.Portfolio;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Features.Commands
{
    public class PopulatePortfolioCategoryListCommand : IRequest
    {
        public IQueryable<Portfolio> Portfolios { get; set; }
        public List<PortfolioViewModel> ViewModel { get; set; }
    }
}
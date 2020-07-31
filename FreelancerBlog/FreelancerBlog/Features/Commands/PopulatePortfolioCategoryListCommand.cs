using System.Collections.Generic;
using System.Linq;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Web.Areas.Admin.ViewModels.Portfolio;
using MediatR;

namespace FreelancerBlog.Web.Features.Commands
{
    public class PopulatePortfolioCategoryListCommand : IRequest
    {
        public IQueryable<Portfolio> Portfolios { get; set; }
        public List<PortfolioViewModel> ViewModel { get; set; }
    }
}
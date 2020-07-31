using System.Linq;
using MediatR;

namespace FreelancerBlog.Web.Features.Commands
{
    public class PopulatePortfolioCategoryListCommandHandler : RequestHandler<PopulatePortfolioCategoryListCommand>
    {
        protected override void Handle(PopulatePortfolioCategoryListCommand message)
        {
            message.ViewModel.ForEach(v => v.PortfolioCategoryList = message.Portfolios.Single(p => p.PortfolioId == v.PortfolioId).PortfolioCategory.Split(',').ToList());
        }
    }
}
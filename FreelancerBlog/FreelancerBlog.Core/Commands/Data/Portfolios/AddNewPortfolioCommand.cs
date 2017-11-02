using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.Portfolios
{
    public class AddNewPortfolioCommand : IRequest
    {
        public Portfolio Portfolio { get; set; }
    }
}

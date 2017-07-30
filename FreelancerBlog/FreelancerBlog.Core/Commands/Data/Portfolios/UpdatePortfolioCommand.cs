using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.Portfolios
{
    public class UpdatePortfolioCommand : IRequest
    {
        public Portfolio Portfolio { get; set; }
    }
}
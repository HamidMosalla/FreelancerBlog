using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Portfolios
{
    public class PortfolioByIdQuery: IRequest<Portfolio>
    {
        public int PortfolioId { get; set; }
    }
}
using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Portfolios
{
    public class PortfolioByIdQuery: IRequest<Portfolio>
    {
        public int PortfolioId { get; set; }
    }
}
using System.Linq;
using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Portfolios
{
    public class GetAllPortfoliosQuery:IRequest<IQueryable<Portfolio>> { }
}
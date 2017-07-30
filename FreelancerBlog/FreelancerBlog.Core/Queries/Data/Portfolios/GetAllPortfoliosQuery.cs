using System.Linq;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Portfolios
{
    public class GetAllPortfoliosQuery:IRequest<IQueryable<Portfolio>> { }
}
using System.Linq;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Articles
{
    public class GetAriclesQuery : IRequest<IQueryable<Domain.Article>>{ }
}
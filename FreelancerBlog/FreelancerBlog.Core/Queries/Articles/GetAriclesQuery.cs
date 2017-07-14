using System.Linq;
using MediatR;

namespace FreelancerBlog.Core.Queries.Articles
{
    public class GetAriclesQuery : IRequest<IQueryable<Domain.Article>>{ }
}
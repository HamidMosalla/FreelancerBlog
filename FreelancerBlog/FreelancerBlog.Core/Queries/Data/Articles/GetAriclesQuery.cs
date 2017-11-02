using System.Linq;
using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Articles
{
    public class GetAriclesQuery : IRequest<IQueryable<Article>>{ }
}
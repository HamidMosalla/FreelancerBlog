using System.Linq;
using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Contacts
{
    public class GetAllContactQuery : IRequest<IQueryable<Contact>> { }
}
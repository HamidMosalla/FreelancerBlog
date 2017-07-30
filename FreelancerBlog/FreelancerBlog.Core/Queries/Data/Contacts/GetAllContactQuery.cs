using System.Linq;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Contacts
{
    public class GetAllContactQuery : IRequest<IQueryable<Contact>> { }
}
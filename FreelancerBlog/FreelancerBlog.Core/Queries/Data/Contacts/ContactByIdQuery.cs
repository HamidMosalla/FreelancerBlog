using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.Contacts
{
    public class ContactByIdQuery : IRequest<Contact>
    {
        public int ContactId { get; set; }
    }
}
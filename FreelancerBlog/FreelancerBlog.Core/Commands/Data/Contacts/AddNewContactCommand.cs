using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.Contacts
{
    public class AddNewContactCommand : IRequest
    {
        public Contact Contact { get; set; }
    }
}
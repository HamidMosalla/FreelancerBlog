using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.ApplicationUsers
{
    public class UserByIdQuery : IRequest<ApplicationUser>
    {
        public string ApplicationUserId { get; set; }
    }
}
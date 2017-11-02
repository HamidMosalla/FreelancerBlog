using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.ApplicationUsers
{
    public class UserByUserNameQuery : IRequest<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.ApplicationUsers
{
    public class UpdateUserProfileCommand : IRequest
    {
        public ApplicationUser ApplicationUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.ApplicationUsers
{
    public class UpdateUserProfileCommand : IRequest
    {
        public ApplicationUser ApplicationUser { get; set; }
    }
}

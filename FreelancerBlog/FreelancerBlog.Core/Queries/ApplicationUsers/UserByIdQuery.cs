using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.ApplicationUsers
{
    public class UserByIdQuery : IRequest<ApplicationUser>
    {
        public string ApplicationUserId { get; set; }
    }
}
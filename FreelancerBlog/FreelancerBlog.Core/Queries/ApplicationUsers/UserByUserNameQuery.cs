using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.ApplicationUsers
{
    public class UserByUserNameQuery : IRequest<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}
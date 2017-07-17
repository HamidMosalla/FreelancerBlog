using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Contacts
{
    public class AddNewContactCommand : IRequest
    {
        public Contact Contact { get; set; }
    }
}
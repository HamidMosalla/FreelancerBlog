using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Contacts
{
    public class DeleteContactCommand: IRequest
    {
        public Contact Contact { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Contacts
{
    public class ContactByIdQuery : IRequest<Contact>
    {
        public int ContactId { get; set; }
    }
}
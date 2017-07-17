using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Contacts
{
    public class GetAllContactQuery : IRequest<IQueryable<Contact>> { }
}
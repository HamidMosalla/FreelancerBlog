using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Portfolios
{
    public class GetAllPortfoliosQuery:IRequest<IQueryable<Portfolio>> { }
}
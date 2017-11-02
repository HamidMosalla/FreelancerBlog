using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Types;
using MediatR;

namespace FreelancerBlog.Core.Queries.Services.SiteOrder
{
    public class FinalPriceQuery  : IRequest<decimal>
    {
        public List<PriceSpec> PriceSpecs { get; set; }
    }
}

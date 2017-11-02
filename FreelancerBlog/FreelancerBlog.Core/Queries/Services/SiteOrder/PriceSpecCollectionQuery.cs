using System.Collections.Generic;
using FreelancerBlog.Core.Types;
using MediatR;

namespace FreelancerBlog.Core.Queries.Services.SiteOrder
{
    public class PriceSpecCollectionQuery : IRequest<List<PriceSpec>>
    {
        public object ViewModel { get; set; }
    }
}

using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.SiteOrders
{
    public class AddSiteOrderCommand : IRequest
    {
        public SiteOrder SiteOrder { get; set; }
    }
}
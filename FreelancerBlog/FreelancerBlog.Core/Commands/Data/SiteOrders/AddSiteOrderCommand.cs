using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.SiteOrders
{
    public class AddSiteOrderCommand : IRequest
    {
        public SiteOrder SiteOrder { get; set; }
    }
}
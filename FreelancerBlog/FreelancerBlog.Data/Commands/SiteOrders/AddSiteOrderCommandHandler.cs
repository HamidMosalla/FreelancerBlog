using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.SiteOrders;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.SiteOrders
{
    public class AddSiteOrderCommandHandler : IAsyncRequestHandler<AddSiteOrderCommand>
    {
        private FreelancerBlogContext _context;

        public AddSiteOrderCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(AddSiteOrderCommand message)
        {
            _context.SiteOrders.Add(message.SiteOrder);

            return _context.SaveChangesAsync();
        }
    }
}
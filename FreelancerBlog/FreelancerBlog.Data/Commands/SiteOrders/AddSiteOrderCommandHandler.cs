using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.SiteOrders;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.SiteOrders
{
    public class AddSiteOrderCommandHandler : AsyncRequestHandler<AddSiteOrderCommand>
    {
        private FreelancerBlogContext _context;

        public AddSiteOrderCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task HandleCore(AddSiteOrderCommand message)
        {
            _context.SiteOrders.Add(message.SiteOrder);

            return _context.SaveChangesAsync();
        }
    }
}
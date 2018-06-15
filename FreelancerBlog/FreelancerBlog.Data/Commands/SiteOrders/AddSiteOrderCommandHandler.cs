using System.Threading;
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

        protected override  Task Handle(AddSiteOrderCommand request, CancellationToken cancellationToken)
        {
            _context.SiteOrders.Add(request.SiteOrder);

            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;

namespace FreelancerBlog.Core.Repository
{
    public interface ISiteOrderRepository : IRepository<SiteOrder, int>
    {
        Task<int> AddSiteOrderAsync(SiteOrder siteOrder);
    }
}

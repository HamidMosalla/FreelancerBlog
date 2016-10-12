using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface ISiteOrderRepository : IRepository<SiteOrder, int>
    {
        Task<int> AddSiteOrderAsync(SiteOrder siteOrder);
    }
}

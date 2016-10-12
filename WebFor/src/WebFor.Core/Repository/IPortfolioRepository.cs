using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IPortfolioRepository : IRepository<Portfolio, int>
    {
        Task<int> AddNewPortfolio(Portfolio portfolio);
        Task<int> DeletePortfolioAsync(Portfolio model);
        void Detach(Portfolio model);
        Task<int> UpdatePortfolioAsync(Portfolio portfolio);
    }
}

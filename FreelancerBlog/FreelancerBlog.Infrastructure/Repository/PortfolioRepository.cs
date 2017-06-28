using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Infrastructure.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private FreelancerBlogContext _context;

        public PortfolioRepository(FreelancerBlogContext context)
        {
            _context = context;
        }


        public void Add(Portfolio entity)
        {
            _context.Portfolios.Add(entity);
        }

        public void Remove(Portfolio entity)
        {
            _context.Portfolios.Remove(entity);
        }

        public void Update(Portfolio entity)
        {
            _context.Portfolios.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task<Portfolio> FindByIdAsync(int id)
        {
            return _context.Portfolios.SingleOrDefaultAsync(p=>p.PortfolioId.Equals(id));
        }

        public Task<List<Portfolio>> GetAllAsync()
        {
            return _context.Portfolios.OrderByDescending(p=>p.PortfolioDateCreated).ToListAsync();
        }

        public Task<int> AddNewPortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Add(portfolio);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeletePortfolioAsync(Portfolio model)
        {
            _context.Portfolios.Remove(model);
            return _context.SaveChangesAsync();
        }

        public void Detach(Portfolio model)
        {
            _context.Entry(model).State = EntityState.Detached;
        }

        public Task<int> UpdatePortfolioAsync(Portfolio portfolio)
        {
            _context.Portfolios.Attach(portfolio);

            var entity = _context.Entry(portfolio);
            entity.State = EntityState.Modified;

            entity.Property(e => e.PortfolioDateCreated).IsModified = false;
            entity.Property(e => e.PortfolioThumbnail).IsModified = portfolio.PortfolioThumbnail != null;

            return _context.SaveChangesAsync();
        }
    }
}

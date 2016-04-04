using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;

namespace WebFor.Infrastructure.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private WebForDbContext _context;

        public PortfolioRepository(WebForDbContext context)
        {
            _context = context;
        }
        public void Add(Portfolio entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Portfolio entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Portfolio entity)
        {
            throw new NotImplementedException();
        }

        public Portfolio FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Portfolio> FindByIdAsync(int id)
        {
            return _context.Portfolios.SingleOrDefaultAsync(p=>p.PortfolioId.Equals(id));
        }

        public IEnumerable<Portfolio> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Portfolio>> GetAllAsync()
        {
            return _context.Portfolios.ToListAsync();
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
            _context.Portfolios.Attach(portfolio, GraphBehavior.SingleObject);

            var entity = _context.Entry(portfolio);
            entity.State = EntityState.Modified;

            entity.Property(e => e.PortfolioDateCreated).IsModified = false;
            entity.Property(e => e.PortfolioThumbnail).IsModified = portfolio.PortfolioThumbnail != null;

            return _context.SaveChangesAsync();
        }
    }
}

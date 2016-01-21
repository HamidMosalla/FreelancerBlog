using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Models;

namespace WebFor.Repositories
{
    public class PortfolioRepository:IPortfolioRepository
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

        public void AddAsync(Portfolio entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Portfolio entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(Portfolio entity)
        {
            throw new NotImplementedException();
        }

        public Portfolio FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Portfolio> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Portfolio> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Portfolio>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}

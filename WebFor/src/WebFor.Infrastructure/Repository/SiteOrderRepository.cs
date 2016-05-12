using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;

namespace WebFor.Infrastructure.Repository
{
    public class SiteOrderRepository : ISiteOrderRepository
    {
        private WebForDbContext _context;

        public SiteOrderRepository(WebForDbContext context)
        {
            _context = context;
        }
        public void Add(SiteOrder entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(SiteOrder entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SiteOrder entity)
        {
            throw new NotImplementedException();
        }

        public SiteOrder FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SiteOrder> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SiteOrder> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<SiteOrder>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> AddSiteOrderAsync(SiteOrder siteOrder)
        {
            _context.SiteOrders.Add(siteOrder);
            return _context.SaveChangesAsync();
        }
    }
}

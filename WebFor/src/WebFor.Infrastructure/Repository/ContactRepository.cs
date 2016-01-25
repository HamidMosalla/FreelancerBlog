using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;

namespace WebFor.Infrastructure.Repository
{
    public class ContactRepository:IContactRepository
    {
        private WebForDbContext _context;

        public ContactRepository(WebForDbContext context)
        {
            _context = context;
        }
        public void Add(Contact entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Contact entity)
        {
            throw new NotImplementedException();
        }

        public Contact FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Contact>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Models;

namespace WebFor.Repositories
{
    public class UserRepository : IUserRepository
    {
        private WebForDbContext _context;

        public UserRepository(WebForDbContext context)
        {
            _context = context;
        }
        public void Add(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser FindById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

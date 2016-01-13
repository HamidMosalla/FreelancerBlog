using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Models;

namespace WebFor.Repositories
{
    public class SlideShowRepository : ISlideShowRepository
    {
        private ApplicationDbContext _context;

        public SlideShowRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(SlideShow entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(SlideShow entity)
        {
            throw new NotImplementedException();
        }

        public SlideShow FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SlideShow> GetAll(Func<SlideShow, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}

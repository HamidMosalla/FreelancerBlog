using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;

namespace WebFor.Infrastructure.Repository
{
    public class SlideShowRepository : ISlideShowRepository
    {
        private WebForDbContext _context;

        public SlideShowRepository(WebForDbContext context)
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

        public Task<SlideShow> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SlideShow> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<SlideShow>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Update(SlideShow entity)
        {
            throw new NotImplementedException();
        }

        public SlideShow FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SlideShow> FindByIdAsync(int id)
        {
            return _context.SlideShows.SingleOrDefaultAsync(s => s.SlideShowId.Equals(id));
        }

        public IEnumerable<SlideShow> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<SlideShow>> GetAllAsync()
        {
            return _context.SlideShows.OrderByDescending(s => s.SlideShowDateCreated).ToListAsync();
        }

        public Task<int> AddNewSlideShow(SlideShow slideShow)
        {
            _context.SlideShows.Add(slideShow);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteSlideShowAsync(SlideShow model)
        {
            _context.SlideShows.Remove(model);

            return _context.SaveChangesAsync();
        }

        public Task<List<SlideShow>> GetAllAsyncForHomePage()
        {
            return _context.SlideShows.OrderBy(s => s.SlideShowPriority).ThenByDescending(s => s.SlideShowDateCreated).Take(10).ToListAsync();
        }

        public Task<int> UpdateSlideShowAsync(SlideShow slideshow)
        {
            _context.SlideShows.Attach(slideshow, GraphBehavior.SingleObject);

            var entity = _context.Entry(slideshow);
            entity.State = EntityState.Modified;

            entity.Property(e => e.SlideShowDateCreated).IsModified = false;
            entity.Property(e => e.SlideShowPictrure).IsModified = slideshow.SlideShowPictrure != null;

            return _context.SaveChangesAsync();
        }

        public void Detach(SlideShow model)
        {
            _context.Entry(model).State = EntityState.Detached;
        }
    }
}

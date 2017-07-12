using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Infrastructure.Repository
{
    public class SlideShowRepository : ISlideShowRepository
    {
        private FreelancerBlogContext _context;

        public SlideShowRepository(FreelancerBlogContext context)
        {
            _context = context;
        }

        public void Add(SlideShow entity)
        {
            _context.SlideShows.Add(entity);
        }

        public void Remove(SlideShow entity)
        {
            _context.SlideShows.Remove(entity);
        }

        public void Update(SlideShow entity)
        {
            _context.SlideShows.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task<SlideShow> FindByIdAsync(int id)
        {
            return _context.SlideShows.SingleOrDefaultAsync(s => s.SlideShowId.Equals(id));
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
            return
                _context.SlideShows.OrderBy(s => s.SlideShowPriority)
                    .ThenByDescending(s => s.SlideShowDateCreated)
                    .Take(10)
                    .ToListAsync();
        }

        public Task<int> UpdateSlideShowAsync(SlideShow slideshow)
        {
            _context.SlideShows.Attach(slideshow);

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

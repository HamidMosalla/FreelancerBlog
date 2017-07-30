using System.Linq;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.SlideShows;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Queries.SlideShows
{
    public class GetAllSlideShowForHomePageQueryHandler : IRequestHandler<GetAllSlideShowForHomePageQuery, IQueryable<SlideShow>>
    {
        private FreelancerBlogContext _context;

        public GetAllSlideShowForHomePageQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public IQueryable<SlideShow> Handle(GetAllSlideShowForHomePageQuery message)
        {
            return _context.SlideShows.OrderBy(s => s.SlideShowPriority)
                                      .ThenByDescending(s => s.SlideShowDateCreated)
                                      .Take(10)
                                      .AsQueryable();
        }
    }
}
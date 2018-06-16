using System.Linq;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.SlideShows;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Queries.SlideShows
{
    public class GetAllSlideShowForHomePageQueryHandler : RequestHandler<GetAllSlideShowForHomePageQuery, IQueryable<SlideShow>>
    {
        private FreelancerBlogContext _context;

        public GetAllSlideShowForHomePageQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override IQueryable<SlideShow> Handle(GetAllSlideShowForHomePageQuery request)
        {
            return _context.SlideShows.OrderBy(s => s.SlideShowPriority)
                .ThenByDescending(s => s.SlideShowDateCreated)
                .Take(10)
                .AsQueryable();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.SlideShows;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Queries.SlideShows
{
    public class AllSlideShowsQueryHandler : IRequestHandler<AllSlideShowsQuery, IQueryable<SlideShow>>
    {
        private FreelancerBlogContext _context;

        public AllSlideShowsQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public IQueryable<SlideShow> Handle(AllSlideShowsQuery message)
        {
           return _context.SlideShows.AsQueryable();
        }
    }
}
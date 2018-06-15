using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.SlideShows;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Queries.SlideShows
{
    public class AllSlideShowsQueryHandler : RequestHandler<AllSlideShowsQuery, IQueryable<SlideShow>>
    {
        private FreelancerBlogContext _context;

        public AllSlideShowsQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override IQueryable<SlideShow> Handle(AllSlideShowsQuery message)
        {
           return _context.SlideShows.AsQueryable();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.SlideShows;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.SlideShows
{
    public class SlideShowByIdQueryHandler : IAsyncRequestHandler<SlideShowByIdQuery, SlideShow>
    {
        private FreelancerBlogContext _context;

        public SlideShowByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task<SlideShow> Handle(SlideShowByIdQuery message)
        {
           return _context.SlideShows.SingleAsync(s => s.SlideShowId == message.SlideShowId);
        }
    }
}
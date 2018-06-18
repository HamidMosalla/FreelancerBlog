using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Core.Queries.Data.SlideShows;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Queries.SlideShows
{
    public class SlideShowByIdQueryHandler : IRequestHandler<SlideShowByIdQuery, SlideShow>
    {
        private readonly FreelancerBlogContext _context;

        public SlideShowByIdQueryHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public  Task<SlideShow> Handle(SlideShowByIdQuery request, CancellationToken cancellationToken)
        {
            return _context.SlideShows.SingleAsync(s => s.SlideShowId == request.SlideShowId, cancellationToken: cancellationToken);
        }
    }
}
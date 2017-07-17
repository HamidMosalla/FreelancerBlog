using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.SlideShows
{
    public class SlideShowByIdQuery : IRequest<SlideShow>
    {
        public int SlideShowId { get; set; }
    }
}
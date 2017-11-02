using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.SlideShows
{
    public class SlideShowByIdQuery : IRequest<SlideShow>
    {
        public int SlideShowId { get; set; }
    }
}
using System.Linq;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.SlideShows
{
    public class GetAllSlideShowForHomePageQuery : IRequest<IQueryable<SlideShow>> { }
}
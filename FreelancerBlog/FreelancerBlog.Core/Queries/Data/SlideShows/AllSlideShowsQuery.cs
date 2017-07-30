using System.Linq;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.SlideShows
{
    public class AllSlideShowsQuery : IRequest<IQueryable<SlideShow>> { }
}
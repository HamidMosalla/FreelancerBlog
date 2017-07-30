using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.SlideShows
{
    public class DeleteSlideShowCommand : IRequest
    {
        public SlideShow SlideShow { get; set; }
    }
}
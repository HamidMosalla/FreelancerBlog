using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.SlideShows;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.SlideShows
{
    public class DeleteSlideShowCommandHandler : IAsyncRequestHandler<DeleteSlideShowCommand>
    {
        private FreelancerBlogContext _context;

        public DeleteSlideShowCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(DeleteSlideShowCommand message)
        {
            _context.SlideShows.Remove(message.SlideShow);

            return _context.SaveChangesAsync();
        }
    }
}
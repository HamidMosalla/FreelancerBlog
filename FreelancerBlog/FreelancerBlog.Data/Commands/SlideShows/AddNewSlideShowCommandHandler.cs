using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.SlideShows;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.SlideShows
{
    public class AddNewSlideShowCommandHandler : IAsyncRequestHandler<AddNewSlideShowCommand>
    {
        private FreelancerBlogContext _context;

        public AddNewSlideShowCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(AddNewSlideShowCommand message)
        {
            _context.SlideShows.Add(message.SlideShow);
            return _context.SaveChangesAsync();
        }
    }
}
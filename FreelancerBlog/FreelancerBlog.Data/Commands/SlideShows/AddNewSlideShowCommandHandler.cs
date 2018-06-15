using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.SlideShows;
using FreelancerBlog.Data.EntityFramework;
using MediatR;

namespace FreelancerBlog.Data.Commands.SlideShows
{
    public class AddNewSlideShowCommandHandler : AsyncRequestHandler<AddNewSlideShowCommand>
    {
        private FreelancerBlogContext _context;

        public AddNewSlideShowCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override  Task Handle(AddNewSlideShowCommand request, CancellationToken cancellationToken)
        {
            _context.SlideShows.Add(request.SlideShow);
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
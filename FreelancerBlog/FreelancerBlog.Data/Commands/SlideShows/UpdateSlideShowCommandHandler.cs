using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.SlideShows;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.SlideShows
{
    public class UpdateSlideShowCommandHandler : IAsyncRequestHandler<UpdateSlideShowCommand>
    {
        private FreelancerBlogContext _context;

        public UpdateSlideShowCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        public Task Handle(UpdateSlideShowCommand message)
        {
            _context.SlideShows.Attach(message.SlideShow);

            var entity = _context.Entry(message.SlideShow);
            entity.State = EntityState.Modified;

            entity.Property(e => e.SlideShowDateCreated).IsModified = false;
            entity.Property(e => e.SlideShowPictrure).IsModified = message.SlideShow.SlideShowPictrure != null;

            return _context.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.SlideShows
{
    public class AddNewSlideShowCommand : IRequest
    {
        public SlideShow SlideShow { get; set; }
    }
}
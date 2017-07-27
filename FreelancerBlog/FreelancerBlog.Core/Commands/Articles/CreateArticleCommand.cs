using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Articles
{
    public class CreateArticleCommand : IRequest
    {
        public Article Article { get; set; }
    }
}
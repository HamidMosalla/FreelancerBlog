using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.ArticleTags
{
    public class CreateArticleTagsCommand : IRequest
    {
        public Article Article { get; set; }
        public string ArticleTags { get; set; }
    }
}
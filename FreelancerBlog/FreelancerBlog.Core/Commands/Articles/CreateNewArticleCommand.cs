using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Enums;
using MediatR;

namespace FreelancerBlog.Core.Commands.Articles
{
    public class CreateNewArticleCommand : IRequest<List<ArticleStatus>>
    {
        public Article Article { get; set; }
        public string ArticleTags { get; set; }
    }
}
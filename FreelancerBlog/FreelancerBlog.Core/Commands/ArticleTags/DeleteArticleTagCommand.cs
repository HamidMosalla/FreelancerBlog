using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.ArticleTags
{
    public class DeleteArticleTagCommand : IRequest
    {
        public ArticleTag ArticleTag { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace FreelancerBlog.Core.Commands.Articles
{
    public class IncreaseArticleViewCountCommand : IRequest
    {
        public int ArticleId { get; set; }
    }
}
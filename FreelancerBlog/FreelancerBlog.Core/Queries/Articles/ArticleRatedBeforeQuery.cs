using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace FreelancerBlog.Core.Queries.Articles
{
    public class ArticleRatedBeforeQuery : IRequest<bool>
    {
        public int ArticleId { get; set; }
        public string UserId { get; set; }
    }
}
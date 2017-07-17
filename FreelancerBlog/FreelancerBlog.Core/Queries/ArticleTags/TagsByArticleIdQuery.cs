using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace FreelancerBlog.Core.Queries.ArticleTags
{
    public class TagsByArticleIdQuery : IRequest<string>
    {
        public int ArticleId { get; set; }
    }
}
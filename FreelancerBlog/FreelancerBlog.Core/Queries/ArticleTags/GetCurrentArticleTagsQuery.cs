using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.ArticleTags
{
    public class GetCurrentArticleTagsQuery : IRequest<List<ArticleTag>>
    {
        public int ArticleId { get; set; }
    }
}
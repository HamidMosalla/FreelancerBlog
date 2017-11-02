using System.Collections.Generic;
using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.ArticleTags
{
    public class GetCurrentArticleTagsQuery : IRequest<List<ArticleTag>>
    {
        public int ArticleId { get; set; }
    }
}
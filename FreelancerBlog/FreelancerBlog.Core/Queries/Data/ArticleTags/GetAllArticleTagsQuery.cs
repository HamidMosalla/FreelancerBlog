using System.Linq;
using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.ArticleTags
{
    public class GetAllArticleTagsQuery : IRequest<IQueryable<ArticleTag>> { }
}
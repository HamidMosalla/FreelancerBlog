using System.Linq;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.Data.ArticleTags
{
    public class GetAllArticleTagsQuery : IRequest<IQueryable<ArticleTag>> { }
}
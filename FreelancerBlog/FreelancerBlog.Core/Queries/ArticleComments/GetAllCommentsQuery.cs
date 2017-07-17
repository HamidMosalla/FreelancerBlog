using System.Linq;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.ArticleComments
{
    public class GetAllCommentsQuery: IRequest<IQueryable<ArticleComment>> { }
}
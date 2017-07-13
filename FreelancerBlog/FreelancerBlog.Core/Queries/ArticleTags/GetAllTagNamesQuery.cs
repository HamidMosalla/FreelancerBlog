using MediatR;

namespace FreelancerBlog.Core.Queries.ArticleTags
{
    public class GetAllTagNamesQuery : IRequest<string[]> { }
}
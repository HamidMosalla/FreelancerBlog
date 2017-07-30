using MediatR;

namespace FreelancerBlog.Core.Queries.Data.ArticleTags
{
    public class GetAllTagNamesQuery : IRequest<string[]> { }
}
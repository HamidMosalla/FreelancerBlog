using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.Data.ArticleTags
{
    public class EditArticleTagCommand : IRequest
    {
        public ArticleTag ArticleTag { get; set; }
        public string NewTagName { get; set; }
    }
}
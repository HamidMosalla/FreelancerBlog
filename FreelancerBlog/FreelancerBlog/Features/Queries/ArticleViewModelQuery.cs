using System.Security.Claims;
using FreelancerBlog.Areas.Admin.ViewModels.Article;
using FreelancerBlog.Core.DomainModels;
using MediatR;

namespace FreelancerBlog.Features.Queries
{
    public class ArticleViewModelQuery : IRequest<ArticleViewModel>
    {
        public Article Article { get; set; }
        public ClaimsPrincipal User { get; set; }
    }
}
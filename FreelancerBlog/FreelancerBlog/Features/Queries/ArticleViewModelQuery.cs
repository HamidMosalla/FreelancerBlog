using System.Security.Claims;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Web.Areas.Admin.ViewModels.Article;
using MediatR;

namespace FreelancerBlog.Web.Features.Queries
{
    public class ArticleViewModelQuery : IRequest<ArticleViewModel>
    {
        public Article Article { get; set; }
        public ClaimsPrincipal User { get; set; }
    }
}
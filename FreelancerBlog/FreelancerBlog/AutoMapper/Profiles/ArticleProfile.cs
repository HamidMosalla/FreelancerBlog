using AutoMapper;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Web.Areas.Admin.ViewModels.Article;
using FreelancerBlog.Web.ViewModels.Article;

namespace FreelancerBlog.Web.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleViewModel>();
            CreateMap<ArticleViewModel, Article>();
            CreateMap<ArticleCommentViewModel, ArticleComment>();
            CreateMap<ArticleComment, ArticleCommentViewModel>();
            CreateMap<ArticleTag, ArticleTagViewModel>();
            CreateMap<ArticleTagViewModel, ArticleTag>();
        }
    }
}
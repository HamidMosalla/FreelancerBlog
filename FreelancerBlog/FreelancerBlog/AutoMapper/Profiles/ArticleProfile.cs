using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.Article;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.ViewModels.Article;

namespace FreelancerBlog.AutoMapper.Profiles
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
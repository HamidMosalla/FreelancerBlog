using AutoMapper;
using WebFor.Core.Domain;
using WebFor.Core.Services.Shared;
using WebFor.Web.Areas.Admin.ViewModels.Article;

namespace WebFor.Web.Services
{
    public interface IWebForMapper
    {
        ArticleViewModel ArticleToArticleViewModel(Article article);
        Article ArticleViewModelToArticle(ArticleViewModel articleViewModel);
    }

    public class WebForMapper : IWebForMapper
    {
        static readonly MapperConfiguration _autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Article, ArticleViewModel>();
            cfg.CreateMap<ArticleViewModel, Article>();
        });

        public IMapper Mapper = _autoMapperConfig.CreateMapper();

        public ArticleViewModel ArticleToArticleViewModel(Article article)
        {
            return Mapper.Map<Article, ArticleViewModel>(article);
        }

        public Article ArticleViewModelToArticle(ArticleViewModel articleViewModel)
        {
            return Mapper.Map<ArticleViewModel, Article>(articleViewModel);
        }
    }
}
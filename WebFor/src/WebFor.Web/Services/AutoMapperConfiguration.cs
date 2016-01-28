using AutoMapper;
using WebFor.Core.Domain;
using WebFor.Web.Areas.Admin.ViewModels.Article;

namespace WebFor.Web.Services
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration AutoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Article, ArticleViewModel>();
            cfg.CreateMap<ArticleViewModel, Article>();
        });

        public static IMapper Mapper = AutoMapperConfig.CreateMapper();
    }
}
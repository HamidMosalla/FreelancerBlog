using Autofac;
using WebFor.Core.Services.ArticleServices;
using WebFor.Infrastructure.Services.ArticleServices;

namespace WebFor.DependencyInjection.Modules.Article
{
    public class ArticleCreatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ArticleCreator>().As<IArticleCreator>();
        }
    }
}

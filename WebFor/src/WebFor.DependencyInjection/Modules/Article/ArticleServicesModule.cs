using Autofac;
using WebFor.Core.Services.ArticleServices;
using WebFor.Infrastructure.Services.ArticleServices;

namespace WebFor.DependencyInjection.Modules.Article
{
    public class ArticleServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ArticleServices>().As<IArticleServices>();
        }
    }
}

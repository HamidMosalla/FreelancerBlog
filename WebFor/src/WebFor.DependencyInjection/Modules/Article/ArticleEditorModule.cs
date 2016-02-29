using Autofac;
using WebFor.Core.Services.ArticleServices;
using WebFor.Infrastructure.Services.ArticleServices;

namespace WebFor.DependencyInjection.Modules.Article
{
    public class ArticleEditorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ArticleEditor>().As<IArticleEditor>();
        }
    }
}

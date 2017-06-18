using Autofac;
using FreelancerBlog.Core.Services.ArticleServices;
using FreelancerBlog.Infrastructure.Services.ArticleServices;

namespace FreelancerBlog.DependencyInjection.Modules.Article
{
    public class ArticleServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ArticleServices>().As<IArticleServices>();
        }
    }
}
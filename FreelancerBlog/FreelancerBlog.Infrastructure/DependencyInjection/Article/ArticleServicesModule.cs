using Autofac;
using FreelancerBlog.Core.Services.ArticleServices;
using FreelancerBlog.Services.ArticleServices;

namespace FreelancerBlog.Infrastructure.DependencyInjection.Article
{
    public class ArticleServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ArticleServices>().As<IArticleServices>();
        }
    }
}
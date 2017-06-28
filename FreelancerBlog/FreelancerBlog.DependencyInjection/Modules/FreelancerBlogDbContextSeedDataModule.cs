using Autofac;
using FreelancerBlog.Infrastructure.EntityFramework;

namespace FreelancerBlog.DependencyInjection.Modules
{
    public class FreelancerBlogDbContextSeedDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FreelancerBlogContextSeedData>();
        }
    }
}
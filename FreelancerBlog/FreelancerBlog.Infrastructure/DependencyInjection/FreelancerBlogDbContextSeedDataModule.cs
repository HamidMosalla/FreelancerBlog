using Autofac;
using FreelancerBlog.Data.EntityFramework;

namespace FreelancerBlog.Infrastructure.DependencyInjection
{
    public class FreelancerBlogDbContextSeedDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FreelancerBlogContextSeedData>();
        }
    }
}
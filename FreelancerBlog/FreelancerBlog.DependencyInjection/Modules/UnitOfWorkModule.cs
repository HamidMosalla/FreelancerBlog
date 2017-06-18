using Autofac;
using FreelancerBlog.Core.Repository;
using WebFor.Infrastructure.Repository;

namespace FreelancerBlog.DependencyInjection.Modules
{
    public class UnitOfWorkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}

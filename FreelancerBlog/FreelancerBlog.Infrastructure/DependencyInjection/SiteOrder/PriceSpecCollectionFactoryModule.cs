using Autofac;
using FreelancerBlog.Core.Services.SiteOrderServices;
using FreelancerBlog.Services.SiteOrderServices;

namespace FreelancerBlog.Infrastructure.DependencyInjection.SiteOrder
{
    public class PriceSpecCollectionFactoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PriceSpecCollectionFactory>().As<IPriceSpecCollectionFactory<PriceSpec, object>>();
        }
    }
}

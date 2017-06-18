using Autofac;
using FreelancerBlog.Core.Services.SiteOrderServices;
using WebFor.Infrastructure.Services.SiteOrderServices;

namespace FreelancerBlog.DependencyInjection.Modules.SiteOrder
{
    public class PriceSpecCollectionFactoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PriceSpecCollectionFactory>().As<IPriceSpecCollectionFactory<PriceSpec, object>>();
        }
    }
}

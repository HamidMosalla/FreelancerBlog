using Autofac;
using FreelancerBlog.Core.Services.SiteOrderServices;
using FreelancerBlog.Infrastructure.Services.SiteOrderServices;
using WebFor.Infrastructure.Services.SiteOrderServices;

namespace FreelancerBlog.DependencyInjection.Modules.SiteOrder
{
    public class FinalPriceCalculatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FinalPriceCalculator>().As<IFinalPriceCalculator<PriceSpec>>();
        }
    }
}

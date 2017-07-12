using Autofac;
using FreelancerBlog.Core.Services.SiteOrderServices;
using FreelancerBlog.Services.SiteOrderServices;

namespace FreelancerBlog.Infrastructure.DependencyInjection.SiteOrder
{
    public class FinalPriceCalculatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FinalPriceCalculator>().As<IFinalPriceCalculator<PriceSpec>>();
        }
    }
}

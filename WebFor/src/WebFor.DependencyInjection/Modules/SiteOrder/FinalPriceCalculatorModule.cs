using Autofac;
using WebFor.Core.Services.SiteOrderServices;
using WebFor.Infrastructure.Services.SiteOrderServices;

namespace WebFor.DependencyInjection.Modules.SiteOrder
{
    public class FinalPriceCalculatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FinalPriceCalculator>().As<IFinalPriceCalculator<PriceSpec>>();
        }
    }
}

using Autofac;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Services.Shared;

namespace FreelancerBlog.Infrastructure.DependencyInjection
{
    public class CaptchaValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CaptchaValidator>().As<ICaptchaValidator>();
        }
    }
}
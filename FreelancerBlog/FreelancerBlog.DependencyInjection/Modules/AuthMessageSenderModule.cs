using Autofac;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Infrastructure.Services.Shared;

namespace FreelancerBlog.DependencyInjection.Modules
{
    public class AuthMessageSenderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthMessageSender>().As<IEmailSender>();
            builder.RegisterType<AuthMessageSender>().As<ISmsSender>();
        }
    }
}

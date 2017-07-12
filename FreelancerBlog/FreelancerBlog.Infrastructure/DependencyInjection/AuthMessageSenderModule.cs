using Autofac;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Services.Shared;

namespace FreelancerBlog.Infrastructure.DependencyInjection
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
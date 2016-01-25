using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Repository;
using WebFor.Core.Services;
using WebFor.Infrastructure.Repository;
using WebFor.Infrastructure.Services;

namespace WebFor.DependencyInjection.Modules
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

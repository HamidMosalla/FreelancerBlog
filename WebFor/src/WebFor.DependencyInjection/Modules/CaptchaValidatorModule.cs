using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Repository;
using WebFor.Core.Services;
using WebFor.Core.Services.Shared;
using WebFor.Infrastructure.Repository;
using WebFor.Infrastructure.Services;
using WebFor.Infrastructure.Services.Shared;

namespace WebFor.DependencyInjection.Modules
{
    public class CaptchaValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CaptchaValidator>().As<ICaptchaValidator>();
        }
    }
}

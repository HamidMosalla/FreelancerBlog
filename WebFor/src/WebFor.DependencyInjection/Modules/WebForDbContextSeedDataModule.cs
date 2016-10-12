using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;
using WebFor.Infrastructure.Repository;

namespace WebFor.DependencyInjection.Modules
{
    public class WebForDbContextSeedDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebForDbContextSeedData>();
        }
    }
}

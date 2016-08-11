using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebFor.Core.Services.Shared;

namespace WebFor.Infrastructure.Services.Shared
{
    public class ConfigurationBinderWrapper : IConfigurationBinderWrapper
    {
        public IConfiguration Configuration { get; }

        public ConfigurationBinderWrapper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual T GetValue<T>(string key)
        {
            return Configuration.GetValue<T>(key);
        }
    }
}

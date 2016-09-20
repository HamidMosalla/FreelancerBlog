using Microsoft.Extensions.Configuration;
using WebFor.Core.Services.Shared;
using WebFor.Core.Wrappers;

namespace WebFor.Infrastructure.Wrappers
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

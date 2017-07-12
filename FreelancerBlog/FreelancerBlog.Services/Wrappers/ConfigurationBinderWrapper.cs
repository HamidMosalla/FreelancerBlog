using FreelancerBlog.Core.Wrappers;
using Microsoft.Extensions.Configuration;

namespace FreelancerBlog.Services.Wrappers
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
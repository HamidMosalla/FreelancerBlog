using Microsoft.Extensions.Configuration;

namespace FreelancerBlog.Core.Wrappers
{
    public interface IConfigurationBinderWrapper
    {
        IConfiguration Configuration { get; }

        T GetValue<T>(string key);
    }
}

using Microsoft.Extensions.Configuration;

namespace WebFor.Core.Wrappers
{
    public interface IConfigurationBinderWrapper
    {
        IConfiguration Configuration { get; }

        T GetValue<T>(string key);
    }
}

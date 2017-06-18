using Microsoft.Extensions.Logging;

namespace FreelancerBlog.Core.Wrappers
{
    public interface ILoggerFactoryWrapper
    {
        ILoggerFactory LoggerFactory { get; }

        ILogger<T> CreateLogger<T>();
    }
}

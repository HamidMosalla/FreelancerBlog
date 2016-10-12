using Microsoft.Extensions.Logging;

namespace WebFor.Core.Wrappers
{
    public interface ILoggerFactoryWrapper
    {
        ILoggerFactory LoggerFactory { get; }

        ILogger<T> CreateLogger<T>();
    }
}

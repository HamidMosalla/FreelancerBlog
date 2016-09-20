using Microsoft.Extensions.Logging;
using WebFor.Core.Services.Shared;
using WebFor.Core.Wrappers;

namespace WebFor.Infrastructure.Wrappers
{
    public class LoggerFactoryWrapper : ILoggerFactoryWrapper
    {
        public ILoggerFactory LoggerFactory { get; }

        public LoggerFactoryWrapper(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }

        public ILogger<T> CreateLogger<T>()
        {
            return LoggerFactory.CreateLogger<T>();
        }
    }
}

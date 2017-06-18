using FreelancerBlog.Core.Wrappers;
using Microsoft.Extensions.Logging;

namespace FreelancerBlog.Infrastructure.Wrappers
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

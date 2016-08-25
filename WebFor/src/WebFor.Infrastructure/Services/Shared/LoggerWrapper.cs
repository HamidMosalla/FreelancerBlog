using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebFor.Core.Services.Shared;

namespace WebFor.Infrastructure.Services.Shared
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

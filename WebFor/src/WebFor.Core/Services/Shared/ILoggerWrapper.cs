using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebFor.Core.Services.Shared
{
    public interface ILoggerFactoryWrapper
    {
        ILoggerFactory LoggerFactory { get; }

        ILogger<T> CreateLogger<T>();
    }
}

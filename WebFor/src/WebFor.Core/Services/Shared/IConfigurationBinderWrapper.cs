using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebFor.Core.Services.Shared
{
    public interface IConfigurationBinderWrapper
    {
        IConfiguration Configuration { get; }

        T GetValue<T>(string key);
    }
}

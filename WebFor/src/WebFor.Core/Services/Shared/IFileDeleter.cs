using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;

namespace WebFor.Core.Services.Shared
{
    public interface IFileDeleter
    {
        IHostingEnvironment Environment { get; }
        void DeleteFile(string fileName, List<string> path);
    }
}

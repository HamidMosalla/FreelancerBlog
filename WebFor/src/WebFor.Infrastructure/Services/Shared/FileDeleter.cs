using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using WebFor.Core.Services.Shared;

namespace WebFor.Infrastructure.Services.Shared
{
    public class FileDeleter : IFileDeleter
    {
        public FileDeleter(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public IHostingEnvironment Environment { get; }
        public void DeleteFile(string fileName, List<string> path)
        {
            path.Insert(0, Environment.WebRootPath);
            path.Add(fileName);

            var fullPath = Path.Combine(path.ToArray());

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}

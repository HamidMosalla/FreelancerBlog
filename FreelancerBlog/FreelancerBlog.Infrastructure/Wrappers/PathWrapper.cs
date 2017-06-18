using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FreelancerBlog.Core.Wrappers;

namespace WebFor.Infrastructure.Wrappers
{
    public class PathWrapper : IPathWrapper
    {
        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }
    }
}

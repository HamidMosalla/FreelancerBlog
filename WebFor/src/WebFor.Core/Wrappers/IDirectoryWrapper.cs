using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Core.Wrappers
{
    public interface IDirectoryWrapper
    {
        bool Exists(string path);
        DirectoryInfo CreateDirectory(string path);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Wrappers;

namespace WebFor.Core.Wrappers
{
    public interface IFileSystemWrapper
    {
        IFileWrapper File { get; set; }
        IDirectoryWrapper Directory { get; set; }
        IPathWrapper Path { get; set; }
    }
}

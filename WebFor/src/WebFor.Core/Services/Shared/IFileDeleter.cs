using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace WebFor.Core.Services.Shared
{
    public interface IFileDeleter
    {
        IHostingEnvironment Environment { get; }
        void DeleteFile(string fileName, List<string> path);
        void DeleteEditorImages(string bodyText, List<string> path);

    }
}

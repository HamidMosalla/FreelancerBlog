using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFor.Core.Wrappers;

namespace WebFor.Core.Services.Shared
{
    public interface ICkEditorFileUploder
    {
         IUrlHelper UrlHelper { get; }
        IHostingEnvironment Environment { get; }
        IFileSystemWrapper FileSystem { get; }
        Task<string> UploadFromCkEditorAsync(IFormFile file, List<string> path, string ckEditorFuncNum);
    }
}

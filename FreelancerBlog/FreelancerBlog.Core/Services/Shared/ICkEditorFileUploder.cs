using System.Collections.Generic;
using System.Threading.Tasks;
using FreelancerBlog.Core.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Core.Services.Shared
{
    public interface ICkEditorFileUploder
    {
         IUrlHelper UrlHelper { get; }
        IHostingEnvironment Environment { get; }
        IFileSystemWrapper FileSystem { get; }
        Task<string> UploadFromCkEditorAsync(IFormFile file, List<string> path, string ckEditorFuncNum);
    }
}

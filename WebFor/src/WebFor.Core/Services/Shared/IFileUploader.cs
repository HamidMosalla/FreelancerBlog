using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace WebFor.Core.Services.Shared
{
    public interface IFileUploader
    {
        IHostingEnvironment Environment { get; }

        Task<string> UploadFile(IFormFile file, List<string> path);
    }
}

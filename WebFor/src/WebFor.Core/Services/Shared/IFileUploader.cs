using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace WebFor.Core.Services.Shared
{
    public interface IFileUploader
    {
        IHostingEnvironment Environment { get; }

        Task<string> UploadFile(IFormFile file, List<string> path);
    }
}

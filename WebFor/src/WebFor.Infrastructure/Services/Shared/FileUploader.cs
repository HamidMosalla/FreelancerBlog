using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;
using WebFor.Core.Services.Shared;

namespace WebFor.Infrastructure.Services.Shared
{
    public class FileUploader : IFileUploader
    {
        public IHostingEnvironment Environment { get; }

        public FileUploader(IHostingEnvironment environment)
        {
            Environment = environment;
        }


        public async Task<string> UploadFile(IFormFile file, List<string> path)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                var extension = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                var fullFileName = fileName + "-" + DateTime.Now.ToString("yyyyMMdd-HHMMssff") + extension;

                path.Insert(0, Environment.WebRootPath);

                var folderPath = Path.Combine(path.ToArray());

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fullPath = Path.Combine(folderPath, fullFileName);

                await file.SaveAsAsync(fullPath);

                return fullFileName;
            }

            return null;
        }
    }
}

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Wrappers;
using Microsoft.AspNetCore.Http;

namespace FreelancerBlog.Services.Wrappers
{
    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public async Task SaveAsAsync(IFormFile formFile, string fullPath, CancellationToken cancellationToken = new CancellationToken())
        {
            int defaultBufferSize = 80 * 1024;

            if (formFile == null) throw new ArgumentNullException(nameof(formFile));

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                var inputStream = formFile.OpenReadStream();
                await inputStream.CopyToAsync(fileStream, defaultBufferSize, cancellationToken);
            }
        }

    }
}
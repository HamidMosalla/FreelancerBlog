using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebFor.Core.Wrappers;

namespace WebFor.Infrastructure.Wrappers
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

        public async Task SaveAsAsync(IFormFile formFile, string filename, CancellationToken cancellationToken = new CancellationToken())
        {
            int defaultBufferSize = 80 * 1024;

            if (formFile == null)
            {
                throw new ArgumentNullException(nameof(formFile));
            }

            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                var inputStream = formFile.OpenReadStream();
                await inputStream.CopyToAsync(fileStream, defaultBufferSize, cancellationToken);
            }
        }

    }
}

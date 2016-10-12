using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebFor.Core.Wrappers
{
    public interface IFileWrapper
    {
        bool Exists(string path);
        void Delete(string path);
        Task SaveAsAsync(IFormFile formFile, string filename, CancellationToken cancellationToken = default(CancellationToken));
    }
}

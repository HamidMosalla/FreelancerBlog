using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace WebFor.Core.Services.Shared
{
    public interface ICkEditorFileUploder
    {
        Task<string> UploadAsync(IFormFile upload, List<string> webRootPath, string relativePath, string CKEditorFuncNum, string CKEditor,
            string langCode);
    }
}

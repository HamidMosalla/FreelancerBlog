using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebFor.Core.Enums;

namespace WebFor.Core.Services.Shared
{
    public interface IFileManager
    {
        IHostingEnvironment Environment { get; }
        Task<string> UploadFile(IFormFile file, List<string> path);
        FileStatus DeleteFile(string fileName, List<string> path);
        void DeleteEditorImages(string bodyText, List<string> path);
        bool ValidateUploadedFile(IFormFile file, UploadFileType fileType, double maxFileSize, ModelStateDictionary modelState);
    }
}

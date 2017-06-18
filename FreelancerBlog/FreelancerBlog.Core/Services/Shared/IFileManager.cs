using System.Collections.Generic;
using System.Threading.Tasks;
using FreelancerBlog.Core.Enums;
using FreelancerBlog.Core.Wrappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FreelancerBlog.Core.Services.Shared
{
    public interface IFileManager
    {
        IHostingEnvironment Environment { get; }
        IFileSystemWrapper FileSystem { get; }
        Task<string> UploadFileAsync(IFormFile file, List<string> path);
        FileStatus DeleteFile(string fileName, List<string> path);
        void DeleteEditorImages(string bodyText, List<string> path);
        bool ValidateUploadedFile(IFormFile file, UploadFileType fileType, double maxFileSize, ModelStateDictionary modelState);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.ModelBinding;
using WebFor.Core.Enums;

namespace WebFor.Core.Services.Shared
{
    public interface IFileUploadValidator
    {
        bool ValidateUploadedFile(IFormFile file, UploadFileType fileType, double maxFileSize, ModelStateDictionary modelState);
    }
}

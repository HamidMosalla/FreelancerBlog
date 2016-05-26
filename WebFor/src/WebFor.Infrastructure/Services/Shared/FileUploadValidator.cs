using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using WebFor.Core.Services.Shared;
using WebFor.Core.Enums;

namespace WebFor.Infrastructure.Services.Shared
{
    public class FileUploadValidator : IFileUploadValidator
    {
        public bool ValidateUploadedFile(IFormFile file, UploadFileType fileType, double maxFileSize, ModelStateDictionary modelState)
        {

            var extension = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

            if (file.Length > (maxFileSize * (1024 * 1024)))
            {
                modelState.AddModelError(string.Empty, "سایز فایل انتخابی قابل قبول نمیباشد، حداکثر سایز مجاز " + maxFileSize + " مگابایت میباشد.");
                return false;
            }

            if (fileType == UploadFileType.Image)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".JPG", ".JPEG", ".PNG", ".GIF", ".BMP" };

                if (!allowedExtensions.Contains(extension))
                {
                    modelState.AddModelError(string.Empty, "فرمت فایل انتخابی مناسب نیست، فرمت های قابل قبول (jpg, jpeg, png, gif, bmp) میباشند.");
                }

                return allowedExtensions.Contains(extension);
            }

            if (fileType == UploadFileType.DocumentFile)
            {
                var allowedExtensions = new[] { ".doc", ".docx", ".txt", ".xlsx", ".DOC", ".DOCX", ".TXT", ".XLSX" };

                return allowedExtensions.Contains(extension);
            }

            if (fileType == UploadFileType.VideoFile)
            {
                var allowedExtensions = new[] { ".avi", ".mkv", ".flv", ".mp4", ".wmv", ".AVI", ".MKV", ".FLV", ".MP4", ".WMV" };

                return allowedExtensions.Contains(extension);
            }

            if (fileType == UploadFileType.CompressedFile)
            {
                var allowedExtensions = new[] { ".rar", ".zip", ".7zip", ".RAR", ".ZIP", ".7ZIP" };

                return allowedExtensions.Contains(extension);
            }

            return false;
        }

    }
}

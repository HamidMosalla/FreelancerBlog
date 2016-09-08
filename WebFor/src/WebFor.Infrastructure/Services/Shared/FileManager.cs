using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using WebFor.Core.Enums;
using WebFor.Core.Services.Shared;

namespace WebFor.Infrastructure.Services.Shared
{
    public class FileManager : IFileManager
    {
        public IHostingEnvironment Environment { get; }

        public FileManager(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public async Task<string> UploadFile(IFormFile file, List<string> path)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                var extension = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                var fullFileName = fileName.Replace(" ", "-") + "-" + DateTime.Now.ToString("yyyyMMdd-HHMMssff") + extension;

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

        public void DeleteFile(string fileName, List<string> path)
        {
            path.Insert(0, Environment.WebRootPath);
            path.Add(fileName);

            var fullPath = Path.Combine(path.ToArray());

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public void DeleteEditorImages(string bodyText, List<string> path)
        {
            path.Insert(0, Environment.WebRootPath);

            Regex findImages = new Regex(@"<img.*?src=""(.*?)""", RegexOptions.IgnoreCase);

            var result = findImages.Matches(bodyText).Cast<Match>().Select(r => r.Groups[1].Value).ToList();

            if (result.Count != 0)
            {
                var resultFileNames = result.Select(r => r.Split('/').Last());

                foreach (var image in resultFileNames)
                {
                    path.Add(image);

                    var fullPath = Path.Combine(path.ToArray());

                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }

                    path.Remove(path.Last());
                }
            }
        }

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

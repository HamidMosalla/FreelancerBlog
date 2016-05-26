using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using WebFor.Core.Services.Shared;

namespace WebFor.Infrastructure.Services.Shared
{
    public class FileDeleter : IFileDeleter
    {
        public FileDeleter(IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public IHostingEnvironment Environment { get; }
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
    }
}

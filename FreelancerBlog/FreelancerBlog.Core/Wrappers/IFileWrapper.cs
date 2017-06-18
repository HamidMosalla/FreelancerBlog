using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FreelancerBlog.Core.Wrappers
{
    public interface IFileWrapper
    {
        bool Exists(string path);
        void Delete(string path);
        Task SaveAsAsync(IFormFile formFile, string filename, CancellationToken cancellationToken = default(CancellationToken));
    }
}

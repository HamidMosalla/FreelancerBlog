using System.IO;

namespace FreelancerBlog.Core.Wrappers
{
    public interface IDirectoryWrapper
    {
        bool Exists(string path);
        DirectoryInfo CreateDirectory(string path);
    }
}

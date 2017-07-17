using FreelancerBlog.Core.Wrappers;

namespace FreelancerBlog.Services.Wrappers
{
    public class FileSystemWrapper : IFileSystemWrapper
    {
        public IFileWrapper File { get; set; }
        public IDirectoryWrapper Directory { get; set; }
        public IPathWrapper Path { get; set; }

        public FileSystemWrapper(IFileWrapper file, IDirectoryWrapper directory, IPathWrapper path)
        {
            this.File = file;
            this.Directory = directory;
            this.Path = path;
        }
    }
}
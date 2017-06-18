namespace FreelancerBlog.Core.Wrappers
{
    public interface IFileSystemWrapper
    {
        IFileWrapper File { get; set; }
        IDirectoryWrapper Directory { get; set; }
        IPathWrapper Path { get; set; }
    }
}

namespace FreelancerBlog.Core.Wrappers
{
    public interface IPathWrapper
    {
        string GetFileNameWithoutExtension(string path);
        string GetExtension(string path);
        string Combine(params string[] paths);
    }
}

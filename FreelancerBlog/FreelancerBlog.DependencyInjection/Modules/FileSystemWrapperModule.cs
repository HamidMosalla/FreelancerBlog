using Autofac;
using FreelancerBlog.Core.Wrappers;
using FreelancerBlog.Infrastructure.Wrappers;

namespace FreelancerBlog.DependencyInjection.Modules
{
    public class FileSystemWrapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileWrapper>().As<IFileWrapper>().SingleInstance();
            builder.RegisterType<DirectoryWrapper>().As<IDirectoryWrapper>().SingleInstance();
            builder.RegisterType<PathWrapper>().As<IPathWrapper>().SingleInstance();
            builder.RegisterType<FileSystemWrapper>().As<IFileSystemWrapper>().SingleInstance();
        }
    }
}

using Autofac;
using FreelancerBlog.Core.Wrappers;
using FreelancerBlog.Services.Wrappers;

namespace FreelancerBlog.Infrastructure.DependencyInjection
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
using Autofac;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Services.Shared;

namespace FreelancerBlog.Infrastructure.DependencyInjection
{
    public class FileManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileManager>().As<IFileManager>();
        }
    }
}
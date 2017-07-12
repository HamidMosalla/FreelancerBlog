using Autofac;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Infrastructure.Services.Shared;

namespace FreelancerBlog.DependencyInjection.Modules
{
    public class FileManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileManager>().As<IFileManager>();
            builder.RegisterType<FileManager>().As<ICkEditorFileUploder>();
        }
    }
}
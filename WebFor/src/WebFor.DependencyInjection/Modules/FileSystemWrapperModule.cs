using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Repository;
using WebFor.Core.Services;
using WebFor.Core.Services.Shared;
using WebFor.Core.Wrappers;
using WebFor.Infrastructure.Repository;
using WebFor.Infrastructure.Services;
using WebFor.Infrastructure.Services.Shared;
using WebFor.Infrastructure.Wrappers;

namespace WebFor.DependencyInjection.Modules
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

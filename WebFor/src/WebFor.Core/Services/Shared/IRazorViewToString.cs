using System;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebFor.Core.Services.Shared
{
    public interface IRazorViewToString
    {
        IRazorViewEngine ViewEngine { get; }
        ITempDataProvider TempDataProvider { get; }
        IServiceProvider ServiceProvider { get; }
        string Render<TModel>(string name, TModel model);
    }
}
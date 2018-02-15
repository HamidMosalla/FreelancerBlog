using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FreelancerBlog
{
    public static class AspNetCoreGlobalExceptionHandlerExtension
    {
        public static IApplicationBuilder UseAspNetCoreExceptionHandler(this IApplicationBuilder app)
        {
            var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;

            return app.UseExceptionHandler(HandleException(loggerFactory));
        }

        public static Action<IApplicationBuilder> HandleException(ILoggerFactory loggerFactory)
        {
            return appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("Serilog Global exception logger");
                        logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
                    }

                    //var path = context.Response.HttpContext.Request.Path;

                    //if (path.HasValue && path.Value.Contains("api"))
                    //{
                    //    context.Response.StatusCode = 500;
                    //    await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    //    return;
                    //}

                    context.Response.Redirect($"/Error/Status/{context.Response.StatusCode}");
                });
            };
        }
    }
}
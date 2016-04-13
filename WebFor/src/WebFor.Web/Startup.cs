using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Core.Services;
using WebFor.DependencyInjection;
using WebFor.Infrastructure.EntityFramework;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using cloudscribe.Web.Pagination;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Routing;
using WebFor.DependencyInjection.Modules;
using WebFor.DependencyInjection.Modules.Article;
using WebFor.Web.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.WebEncoders;
using WebFor.Infrastructure.Services.Shared;

namespace WebFor.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<WebForDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonLetterOrDigit = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength= 6;
            }).AddEntityFrameworkStores<WebForDbContext>()
              .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddCaching();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.CookieName = ".WebFor";
            });

            services.Configure<AuthMessageSenderSecrets>(Configuration.GetSection("AuthMessageSenderSecrets"));

            services.AddTransient<IUrlHelper, UrlHelper>();
            services.TryAddTransient<IBuildPaginationLinks, PaginationLinkBuilder>();

            // Autofac container configuration and modules
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<UnitOfWorkModule>();
            containerBuilder.RegisterModule<AuthMessageSenderModule>();
            containerBuilder.RegisterModule<WebForDbContextSeedDataModule>();
            containerBuilder.RegisterModule<CkEditorFileUploderModule>();
            containerBuilder.RegisterModule<FileUploderModule>();
            containerBuilder.RegisterModule<FileDeleterModule>();
            containerBuilder.RegisterModule<FileUploadValidatorModule>();
            containerBuilder.RegisterModule<ArticleCreatorModule>();
            containerBuilder.RegisterModule<ArticleEditorModule>();
            containerBuilder.RegisterType<WebForMapper>().As<IWebForMapper>();

            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, WebForDbContextSeedData seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Status/{0}");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<WebForDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseStatusCodePagesWithRedirects("/Error/Status/{0}");

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseGoogleAuthentication(options =>
            {
                options.ClientId = Configuration["OAuth:Google:ClientId"];
                options.ClientSecret = Configuration["OAuth:Google:ClientSecret"];
                options.Events = new OAuthEvents()
                {
                    OnRemoteError = ctx =>

                    {
                        ctx.Response.Redirect("/error?ErrorMessage=" + UrlEncoder.Default.UrlEncode(ctx.Error.Message));
                        ctx.HandleResponse();
                        return Task.FromResult(0);
                    }
                };
            });

            app.UseFacebookAuthentication(options =>
            {
                options.AppId = Configuration["OAuth:Facebook:AppId"];
                options.AppSecret = Configuration["OAuth:Facebook:AppSecret"];
                options.Scope.Add("email");
                options.BackchannelHttpHandler = new FacebookBackChannelHandler();
                options.UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email,first_name,last_name,location";
            });

            app.UseTwitterAuthentication(options =>
            {
                options.ConsumerKey = Configuration["OAuth:Twitter:ConsumerKey"];
                options.ConsumerSecret = Configuration["OAuth:Twitter:ConsumerSecret"];
                options.DisplayName = "WebFor Twitter Auth";
            });

            app.UseMicrosoftAccountAuthentication(options =>
            {
                options.ClientId = Configuration["OAuth:Microsoft:ClientId"];
                options.ClientSecret = Configuration["OAuth:Microsoft:ClientSecret"];
                options.Scope.Add("wl.emails, wl.basic");
                options.DisplayName = "WebFor Microsoft OAuth";
            });

            app.UseSession();

            app.UseMvc(routes =>
            {
                 routes.MapRoute(name: "AreaRoute",
                 template: "{area:exists}/{controller}/{action}/{id?}/{title?}",
                 defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}/{title?}");


            });

            seeder.SeedAdminUser();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}

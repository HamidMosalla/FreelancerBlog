using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using cloudscribe.Web.Pagination;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Services.Shared;
using FreelancerBlog.Core.Wrappers;
using FreelancerBlog.DependencyInjection.Modules;
using FreelancerBlog.DependencyInjection.Modules.Article;
using FreelancerBlog.DependencyInjection.Modules.SiteOrder;
using FreelancerBlog.Infrastructure.EntityFramework;
using FreelancerBlog.Infrastructure.Services.Shared;
using FreelancerBlog.Infrastructure.Wrappers;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FreelancerBlog
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets("aspnet5-freelancerblog-23975498-e4cd-4072-bc80-0fca99fd4a83");
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<FreelancerBlogContext>(options =>
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<FreelancerBlogContext>()
              .AddDefaultTokenProviders();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddAutoMapper();

            services.AddMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.CookieName = ".FreelancerBlog";
            });

            services.Configure<AuthMessageSenderSecrets>(Configuration.GetSection("AuthMessageSenderSecrets"));

            services.AddTransient<IUrlHelperFactory, UrlHelperFactory>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IBuildPaginationLinks, PaginationLinkBuilder>();
            services.AddTransient<HttpClient>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IConfigurationBinderWrapper, ConfigurationBinderWrapper>();
            services.AddSingleton<ILoggerFactoryWrapper, LoggerFactoryWrapper>();
            services.AddScoped<IRazorViewToString, RazorViewToString>();

            // Autofac container configuration and modules
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<UnitOfWorkModule>();
            containerBuilder.RegisterModule<AuthMessageSenderModule>();
            containerBuilder.RegisterModule<FreelancerBlogDbContextSeedDataModule>();
            containerBuilder.RegisterModule<FileManagerModule>();
            containerBuilder.RegisterModule<ArticleServicesModule>();
            containerBuilder.RegisterModule<PriceSpecCollectionFactoryModule>();
            containerBuilder.RegisterModule<FinalPriceCalculatorModule>();
            containerBuilder.RegisterModule<CaptchaValidatorModule>();
            containerBuilder.RegisterModule<FileSystemWrapperModule>();

            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, FreelancerBlogContextSeedData seeder)
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
            }

            app.UseStatusCodePagesWithRedirects("/Error/Status/{0}");

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "FreelancerBlogCookieMiddlewareInstance",
                LoginPath = new PathString("/Account/Login/"),
                AccessDeniedPath = new PathString("/Account/Forbidden/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            #region External Logins Setup

            var googleOption = new GoogleOptions
            {
                ClientId = Configuration["OAuth:Google:ClientId"],
                ClientSecret = Configuration["OAuth:Google:ClientSecret"],
                Events = new OAuthEvents()
                {
                    OnRemoteFailure = ctx =>

                    {
                        ctx.Response.Redirect("/error?ErrorMessage=" + UrlEncoder.Default.Encode(ctx.Failure.Message));
                        ctx.HandleResponse();
                        return Task.FromResult(0);
                    }
                }
            };

            var faceBookOption = new FacebookOptions
            {
                AppId = Configuration["OAuth:Facebook:AppId"],
                AppSecret = Configuration["OAuth:Facebook:AppSecret"],
                //Scope.Add("email"),
                //Scope = new List<string> { "slkjdf"},
                //Scope.Add("email"),
                BackchannelHttpHandler = new FacebookBackChannelHandler(),
                UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email,first_name,last_name,location"
            };

            var twitterOption = new TwitterOptions
            {
                ConsumerKey = Configuration["OAuth:Twitter:ConsumerKey"],
                ConsumerSecret = Configuration["OAuth:Twitter:ConsumerSecret"],
                DisplayName = "FreelancerBlog Twitter Auth"
            };

            var microsoftAccountOptions = new MicrosoftAccountOptions
            {
                ClientId = Configuration["OAuth:Microsoft:ClientId"],
                ClientSecret = Configuration["OAuth:Microsoft:ClientSecret"],
                //Scope.Add("wl.emails, wl.basic"),
                DisplayName = "FreelancerBlog Microsoft OAuth"
            };

            app.UseGoogleAuthentication(googleOption);
            app.UseFacebookAuthentication(faceBookOption);
            app.UseTwitterAuthentication(twitterOption);
            app.UseMicrosoftAccountAuthentication(microsoftAccountOptions);

            #endregion

            app.UseSession();

            var supportedCultures = new[]
            {
                new CultureInfo("fa-IR"),
                new CultureInfo("en-US")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

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
    }
}
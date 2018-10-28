using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Route.Services;

namespace Route
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddSingleton<IRazorViewEngine, InfoSystemViewEngine>();
            services.Replace(ServiceDescriptor.Singleton<IControllerFactory, ControllerFactory>());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/InfoSystem_HomeController/Error");

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMiddleware<TimingService>();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Home page",
                    template: "",
                    defaults: new {controller = "Home", action = "Index"});
                routes.MapRoute(
                    name: "default",
                    template: "InfoSystem_{controller=Home}Controller/{action=Index}/{id?}");
                routes.Routes.Add(new Router(routes.DefaultHandler));
            });
        }
    }
}
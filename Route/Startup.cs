using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Route
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRouting();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();


            var route_builder = new RouteBuilder(app, new Route());
            route_builder.MapRoute("Marearcat", "{controller}/{action}");
            app.UseRouter(route_builder.Build());
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Welcome!");
            });


            //var controller = new Controllers.Marearcat();
            //RouteHandler router = new RouteHandler(controller.PassivePage);
            //var routeBuilder = new RouteBuilder(app, router);
            //routeBuilder.Routes.Add(new Route());
            //routeBuilder.MapRoute("Marearcat", "{controller}/{action}");
            //app.UseRouter(routeBuilder.Build());


            //var ending = new RouteHandler(control.Ending);
            //var endingBuilder = new RouteBuilder(app, ending);
            //endingBuilder.MapRoute("Marearcat", "marearcat/ending");
            //app.UseRouter(endingBuilder.Build());



            //app.UseMvc(routes =>
            //{

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}"
            //        );
            //});
        }
    }
}

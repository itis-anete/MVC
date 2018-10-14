using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route.Routing;

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
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            var routeBuilder = new Routing.RouteBuilder(app);
            routeBuilder.Routes.Add(new Router());
            app.UseMvc(routes =>
            {
                routes.MapRoute("api/get", async context =>
                {
                    await context.Response.WriteAsync("для обработки использован маршрут api/get");
                });

                routes.MapRoute("default",
                    "{controller}/{action}/{id?}",
                    new { controller = "Home", action = "Index" }
                );
            });

        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
             name: "Home",
             template: "",
             defaults: new { controller = "Home", action = "Index" });

            routeBuilder.MapRoute(
                name: "registration",
                template: "registration",
                defaults: new { controller = "Registration", action = "Registration" });

            routeBuilder.MapRoute(
                name: "login",
                template: "login",
                defaults: new { controller = "Login", action = "Login" });

            routeBuilder.MapRoute(
                name: "default",
                template: "{controller}/{action}");
        }

        /* получение в контроллере все параметры маршрута
        public IActionResult Index()
        {
            var controller = RouteData.Values["controller"].ToString();
            var action = RouteData.Values["action"].ToString();
            return Content($"controller: {controller} | action: {action}");
        }*/


        /* This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }*/

        /* This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseMvc(routes =>
            {
                routes.MapRoute("api", "api/get", new { controller = "Home", action = "About" });

                    routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                    //app.UseMvcWithDefaultRoute();
            });

            
        }*/
    }
}

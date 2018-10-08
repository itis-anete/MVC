using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;

namespace Route
{
    public class RouterMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IRouter router;

        public RouterMiddleware(RequestDelegate next, IRouter router)
        {
            this.next = next;
            this.router = router;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var routeContext = new RouteContext(httpContext);
            routeContext.RouteData.Routers.Add(router);

            await router.RouteAsync(routeContext);

            if (routeContext == null)
            {
                await next.Invoke(httpContext);
            }
            else
            {
                httpContext.Features[typeof(IRoutingFeature)] = new RoutingFeature()
                {
                    RouteData = routeContext.RouteData
                };
                await routeContext.Handler(routeContext.HttpContext);
            }
        }
    }

    public static class RouterExtensions
    {
        public static IApplicationBuilder UseRouting(this IApplicationBuilder app) =>
            app.UseMiddleware<RouterMiddleware>();
    }
}
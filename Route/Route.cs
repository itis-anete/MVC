using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Route
{
    public class Router : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context) => throw new NotImplementedException();

        public Task RouteAsync(RouteContext context)
        {
            var url = context.HttpContext.Request.Path.Value.ToLower();

            context.Handler = url.Contains("leva")
                ? new LevaRouteHandler().GetRequestHandler(context.HttpContext, context.RouteData)
                : url.Contains("anton")
                    ? new AntonRouteHandler().GetRequestHandler(context.HttpContext, context.RouteData)
                    : url.Contains("артур")
                        ? new ArthurRouteHandler().GetRequestHandler(context.HttpContext, context.RouteData)
                        : async httpContext =>
                        {
                            httpContext.Response.ContentType = "text/html;charset=utf-8";
                            await httpContext.Response.WriteAsync("Ты кто такой?");
                        };

            return Task.CompletedTask;
        }
    }

    public class LevaRouteHandler : IRouteHandler
    {
        public RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData)

        {
            var response = httpContext.Request.Path.Value.EndsWith("krasavchik") ? "Лёва krasavchik" : "Лёва teamleader";

            httpContext.Response.ContentType = "text/html;charset=utf-8";

            return async context => await context.Response.WriteAsync(response);
        }
    }

    public class AntonRouteHandler : IRouteHandler
    {
        public RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData)
        {
            var response = httpContext.Request.Path.Value.EndsWith("krasavchik") ? "Антон krasavchik" : "Антон backender";

            httpContext.Response.ContentType = "text/html;charset=utf-8";

            return async context => await context.Response.WriteAsync(response);
        }
    }

    public class ArthurRouteHandler : IRouteHandler
    {
        public RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData)
        {
            var response = httpContext.Request.Path.Value.EndsWith("krasavchik") ? "Артур krasavchik" : "Артур frontdaun";

            httpContext.Response.ContentType = "text/html;charset=utf-8";

            return async context => await context.Response.WriteAsync(response);
        }
    }
}

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
            context.Handler = new RouteHandler().GetRequestHandler(context.HttpContext, context.RouteData);
            return Task.CompletedTask;
        }
    }

    public class RouteHandler : IRouteHandler
    {
        public RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData)
        {
            var url = httpContext.Request.Path.Value.ToLower();
            
            var response = url.Contains("leva")
                ? url.EndsWith("krasavchik") ? "Leva krasavchik" : "Leva teamleader"
                : (url.Contains("anton")
                    ? url.EndsWith("krasavchik") ? "Anton krasavchik" : "Anton backender"
                    : (url.Contains("arthur")
                        ? url.EndsWith("krasavchik") ? "Artur krasavchik" : "Artur frontdaun" 
                        : "Ti kto takoi?"));
            
            return context => context.Response.WriteAsync(response);
        }
    }
}

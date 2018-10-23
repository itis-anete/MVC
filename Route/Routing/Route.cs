using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Route
{
    public class Router : IRouter
    {
        public static ThreadLocal<DateTime> Start;   // ?! 
        
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public Task RouteAsync(RouteContext context)
        {
            var path = context.HttpContext.Request.Path.Value.ToLower();
            context.Handler = path.Contains("d4n0n")
                ? new RouteHandler().GetRequestHandler(context.HttpContext, context.RouteData)
                : async httpContext => { await httpContext.Response.WriteAsync("wtf?"); };
            
            
            return Task.CompletedTask;
        }
    }
}
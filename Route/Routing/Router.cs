using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Route
{
    public class Router : IRouter
    {
        public static ThreadLocal<DateTime> Start; // ?! 

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public Task RouteAsync(RouteContext context)
        {
            Start = new ThreadLocal<DateTime> {Value = DateTime.Now};
            context.Handler = new RouteHandler().GetRequestHandler(context.HttpContext, context.RouteData);
            return Task.CompletedTask;
        }
    }
}
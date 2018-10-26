using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace MarketplaceMVC.Routing
{
    public class Router : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context) => throw new NotImplementedException();

        public Task RouteAsync(RouteContext context)
        {
            var url = context.HttpContext.Request.Path.Value.ToLower();

            return Task.CompletedTask;
        }
    }
}

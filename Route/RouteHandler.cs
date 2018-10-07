using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Route
{
    public class RouteHandler : IRouteHandler, IRouter
    {
        private readonly RequestDelegate _requestDelegate;

        public RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData)
        {
            return _requestDelegate;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public Task RouteAsync(RouteContext context)
        {
            context.Handler = _requestDelegate;
            return Task.CompletedTask;
        }

        public RouteHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
    }
}

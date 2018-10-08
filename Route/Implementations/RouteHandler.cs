using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Route
{
    public class RouteHandler : IRouteHandler
    {
        private readonly RequestDelegate requestDelegate;

        public RouteHandler(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData)
        {
            return requestDelegate;
        }
    }
}
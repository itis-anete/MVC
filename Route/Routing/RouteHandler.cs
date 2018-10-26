using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Route
{
    public class RouteHandler : IRouteHandler
    {
        private readonly RequestDelegate _requestDelegate;

        public RouteHandler()
        {
        }

        public RouteHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData)
        {
            if (_requestDelegate != null)
                return _requestDelegate;

            var path = httpContext.Request.Path.Value.ToLower();
            return newHttpContext =>
            {
                //newHttpContext.Response.Redirect(path);
                return Task.CompletedTask;
            };
        }
    }
}
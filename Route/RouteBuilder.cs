using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route
{
    public class RouteBuilder : IRouteBuilder
    {
        private IApplicationBuilder app;

        public RouteBuilder(IApplicationBuilder app)
        {
            this.app = app;
        }

        public IApplicationBuilder ApplicationBuilder { get; }

        public IRouter DefaultHandler { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public IList<IRouter> Routes { get; }

        public IRouter Build()
        {
            throw new NotImplementedException();
        }
    }

    public class ROuter : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public async Task RouteAsync(RouteContext context)
        {
            string url = context.HttpContext.Request.Path.Value.TrimEnd('/');
            if (url.StartsWith("/User", StringComparison.OrdinalIgnoreCase))
            {
                await context.HttpContext.Response.WriteAsync("Hello, User!");
            }
        }
    }

    public class RouteHandler : IRouteHandler, IRouter
    {
        private readonly RequestDelegate _requestDelegate;

        public RouteHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

       /* public Task RouteAsync(RouteContext context)
        {
            context.Handler = _requestDelegate;
            return TaskCache.CompletedTask;
        }*/

        RequestDelegate IRouteHandler.GetRequestHandler(HttpContext httpContext, RouteData routeData)
        {
            throw new NotImplementedException();
        }

        VirtualPathData IRouter.GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        Task IRouter.RouteAsync(RouteContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class ConrollerFactory : IControllerFactory
    {
        public object CreateController(ControllerContext context)
        {
            throw new NotImplementedException();
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
            throw new NotImplementedException();
        }
    }

    public class ControllerActivator : IControllerActivator
    {
        public object Create(ControllerContext context)
        {
            throw new NotImplementedException();
        }

        public void Release(ControllerContext context, object controller)
        {
            throw new NotImplementedException();
        }
    }

    public class ActionInvokerProvider : IActionInvokerProvider
    {
        public int Order => throw new NotImplementedException();

        public void OnProvidersExecuted(ActionInvokerProviderContext context)
        {
            throw new NotImplementedException();
        }

        public void OnProvidersExecuting(ActionInvokerProviderContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class ActionDescriptorProvider : IActionDescriptorProvider
    {
        public int Order => throw new NotImplementedException();

        public void OnProvidersExecuted(ActionDescriptorProviderContext context)
        {
            throw new NotImplementedException();
        }

        public void OnProvidersExecuting(ActionDescriptorProviderContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class ActionInvoker : IActionInvoker
    {
        public Task InvokeAsync()
        {
            throw new NotImplementedException();
        }
    }

}

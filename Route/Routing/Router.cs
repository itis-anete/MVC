using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Route.Controllers;

namespace Route
{
    public class Router : IRouter
    {
        public Router(IRouter router)
        {
            _defaultRouter = router ?? throw new ArgumentNullException(nameof(router));
        }
        
        public static ThreadLocal<DateTime> Start { get; } = new ThreadLocal<DateTime>(() => DateTime.Now);

        private readonly IRouter _defaultRouter;
        
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public async Task RouteAsync(RouteContext context)
        {
            Route(context);
            await _defaultRouter.RouteAsync(context); 
        }
        
        private static void Route(RouteContext context)
        {
            var parameters =
                context.HttpContext.Request.Path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries) ??
                throw new ArgumentException(nameof(context.HttpContext.Request.Path));
            if (ArgumentsAreInvalid(parameters)) return;
            var startIndex = "InfoSystem_".Length;
            var controllerName = parameters[0].Substring(startIndex,
                parameters[0].Length - startIndex - "Controller".Length);
            var actionName = parameters[1];
            var methodName = context.HttpContext.Request.Method;
            if (!actionName.StartsWith(methodName)) return;
            actionName = actionName.Substring(methodName.Length);
            context.RouteData.Values["controller"] = controllerName;
            context.RouteData.Values["action"] = actionName;
        }

        private static bool ArgumentsAreInvalid(IReadOnlyList<string> parameters) => parameters == null ||
                                                                                     parameters.Count < 2 ||
                                                                                     parameters.Count == 3 &&
                                                                                     !parameters[2].StartsWith('?') ||
                                                                                     parameters.Count > 3 ||
                                                                                     !parameters[0].StartsWith("InfoSystem_");
    }
}
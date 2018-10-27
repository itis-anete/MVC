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
using Microsoft.Extensions.Logging;
using Route.Controllers;

namespace Route
{
    public class Router : IRouter
    {
        public static ThreadLocal<DateTime> Start { get; private set; } = new ThreadLocal<DateTime>(() => DateTime.Now);

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public Task RouteAsync(RouteContext context)
        {
            /*if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var candidates = _actionSelector.SelectCandidates(context);
            if (candidates == null || candidates.Count == 0)
            {
                //_logger.NoActionsMatched(context.RouteData.Values);
                return Task.CompletedTask;
            }

            var actionDescriptor = _actionSelector.SelectBestCandidate(context, candidates);
            if (actionDescriptor == null)
            {
                //_logger.NoActionsMatched(context.RouteData.Values);
                return Task.CompletedTask;
            }*/

            /*context.Handler = (c) =>
            {
                var routeData = c.GetRouteData();

                var actionContext = new ActionContext(context.HttpContext, routeData, actionDescriptor);
                if (_actionContextAccessor != null)
                {
                    _actionContextAccessor.ActionContext = actionContext;
                }

                var invoker = _actionInvokerFactory.CreateInvoker(actionContext);
                if (invoker == null)
                {
                    throw new InvalidOperationException();
                }

                return invoker.InvokeAsync();
            };*/
            /*var methodName = "";
            var c = context.HttpContext.Request.Path.Value.Split('/');
            try
            {
                methodName = c[2];
            }
            catch(Exception e)
            {
                methodName = "/";
            }

            if (HomeController._Delegates == null)
                new HomeController();
            
            var method = HomeController._Delegates[methodName];
            
            method.Invoke();*/

            var routeData = new RouteData();

            var x = context.HttpContext.Request.Path;
            routeData.Values["controller"] = "Home";
            routeData.Values["action"] = "Index";
            context.RouteData = routeData;
            
            context.Handler = (c) => Task.CompletedTask;
            return Task.CompletedTask;
        }
    }
}
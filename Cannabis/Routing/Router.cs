using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Cannabis.Routing
{
    public class Router : IRouter
    {
        public Router(IActionInvokerFactory actionInvokerFactory, IActionSelector actionSelector)
        {
            _actionInvokerFactory = actionInvokerFactory ?? throw new ArgumentNullException(nameof(actionInvokerFactory));
            _actionSelector = actionSelector ?? throw new ArgumentNullException(nameof(actionSelector));
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // We return null here because we're not responsible for generating the url, the route is.
            return null;
        }

        public Task RouteAsync(RouteContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var candidates = _actionSelector.SelectCandidates(context);
            if (candidates == null || candidates.Count == 0)
                return Task.CompletedTask;

            var actionDescriptor = _actionSelector.SelectBestCandidate(context, candidates);
            if (actionDescriptor == null)
                return Task.CompletedTask;

            context.Handler = httpContext =>
            {
                var actionContext = new ActionContext(context.HttpContext, httpContext.GetRouteData(), actionDescriptor);
                var invoker = _actionInvokerFactory.CreateInvoker(actionContext);
                if (invoker == null)
                    throw new InvalidOperationException("Can not create action invoker");

                return invoker.InvokeAsync();
            };
            return Task.CompletedTask;
        }

        private readonly IActionInvokerFactory _actionInvokerFactory;
        private readonly IActionSelector _actionSelector;
    }
}

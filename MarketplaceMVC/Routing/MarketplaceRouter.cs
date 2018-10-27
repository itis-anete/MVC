using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis;

namespace MarketplaceMVC.Routing
{
    public class MarketplaceRouter : IRouter
    {
        public IRouter BaseRouter { get; }

        public MarketplaceRouter(IRouter baseRouter)
        {
            BaseRouter = baseRouter;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return BaseRouter.GetVirtualPath(context);
        }

        public async Task RouteAsync(RouteContext context)
        {
            var splittedUrl = context.HttpContext.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (splittedUrl == null || splittedUrl.Length < 2 || splittedUrl.Length > 3 || splittedUrl.Length == 3 && !splittedUrl[2].StartsWith('?'))
                return;

            const string controllerNamePrefix = "Marketplace_";

            if (!splittedUrl[0].StartsWith(controllerNamePrefix))
                return;

            var controllerName = splittedUrl[0].Substring(controllerNamePrefix.Length);

            controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);

            var actionName = splittedUrl[1];

            var methodName = context.HttpContext.Request.Method;

            if (!actionName.StartsWith(methodName))
                return;

            actionName = actionName.Substring(methodName.Length);

            context.RouteData.Values["controller"] = controllerName;
            context.RouteData.Values["action"] = actionName;

            await BaseRouter.RouteAsync(context);
        }
    }
}

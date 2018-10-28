using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Cannabis.Routing
{
    public class CannabisRouter : IRouter
    {
        public CannabisRouter(IRouter baseRouter)
        {
            _baseRouter = baseRouter ?? throw new ArgumentNullException(nameof(baseRouter));
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return _baseRouter.GetVirtualPath(context);
        }

        public async Task RouteAsync(RouteContext context)
        {
            context.RouteData.DataTokens["ActionStartTime"] = DateTime.Now;

            var segments = context.HttpContext.Request.Path.Value?
                .Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (segments == null ||
                segments.Length < 2 ||
                segments.Length > 3 ||
                segments.Length == 3 && !segments[2].StartsWith('?'))
                return;

            var controllerNamePrefix = ProjectInfo.ProjectName + '_';
            if (!segments[0].StartsWith(controllerNamePrefix))
                return;

            var controllerNameLength = segments[0].Length
                - controllerNamePrefix.Length
                - "Controller".Length;
            if (controllerNameLength <= 0)
                return;
            var controllerName = segments[0].Substring(
                controllerNamePrefix.Length,
                controllerNameLength
            );

            var actionName = segments[1];
            var methodName = context.HttpContext.Request.Method;
            if (!actionName.StartsWith(methodName))
                return;
            actionName = actionName.Substring(methodName.Length);

            context.RouteData.Values["controller"] = controllerName;
            context.RouteData.Values["action"] = actionName;

            await _baseRouter.RouteAsync(context); 
        }

        private readonly IRouter _baseRouter;
    }
}

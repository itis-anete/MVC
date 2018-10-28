using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class HtmlMethodRouter : IRouter
    {
        private readonly IRouter defaultRouter;

        public HtmlMethodRouter(IRouter defaultRouter)
        {
            this.defaultRouter = defaultRouter;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context) =>
            defaultRouter.GetVirtualPath(context);

        public async Task RouteAsync(RouteContext context)
        {
            Timer.Stopwatch.Reset();
            Timer.Stopwatch.Start();
            var httpMethod = context.HttpContext.Request.Method;
            var urlTokens = context.HttpContext.Request.Path.Value
                .TrimStart('/').TrimEnd('/').Split('/');

            if (urlTokens.Length == 2 && 
                urlTokens[0].StartsWith("Forum_") &&
                urlTokens[0].EndsWith("Controller") &&
                urlTokens[1].StartsWith(httpMethod))
            {
                var controllerClassName = urlTokens[0].Substring(6, urlTokens[0].Length - 26);
                var controllerMethodName = urlTokens[1].Substring(httpMethod.Length);
                var controllerType = Type.GetType($"ForumLesson16.{controllerClassName}Controller", true);
                var attributeType = GetHtmlMethodAttributeType(httpMethod);

                var controllerMethodInfo = controllerType
                    .GetMethods()
                    .FirstOrDefault(method => 
                        method.Name == controllerMethodName && 
                        method.GetCustomAttribute(attributeType) != null);

                if (controllerMethodInfo != null)
                {
                    context.RouteData.Values["controller"] = controllerClassName;
                    context.RouteData.Values["action"] = controllerMethodName;
                    await defaultRouter.RouteAsync(context);
                }
            }
        }

        private Type GetHtmlMethodAttributeType(string methodName)
        {
            switch (methodName)
            {
                case "GET":
                    return typeof(HttpGetAttribute);
                case "POST":
                    return typeof(HttpPostAttribute);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
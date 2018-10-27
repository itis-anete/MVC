using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
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
            var httpMethod = context.HttpContext.Request.Method;
            // GETAction
            var urlTokens = context.HttpContext.Request.Path.Value
                .TrimStart('/')
                .TrimEnd('/')
                .Split('/');

            if (urlTokens.Length == 2)
            {
                context.RouteData.Values["controller"] = urlTokens[0].Substring(6, urlTokens[0].Length - 16);
                context.RouteData.Values["action"] = "";
                await defaultRouter.RouteAsync(context);
            }
        }
    }
}

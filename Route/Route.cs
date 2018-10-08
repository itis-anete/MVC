using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route
{
    public class Route : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public Task RouteAsync(RouteContext context)
        {
            
            //if (context.RouteData.Values.Count != 0 && context.RouteData.Values["{controller}"].ToString().ToLower() == "marearcat")
            //{
                //var controller = new Controllers.Marearcat();
                //switch (context.RouteData.Values["{action}"].ToString().ToLower() ?? "lol")
                //{
                //    case "intro":
                //        return controller.Intro(context.HttpContext);
                //    case "ending":
                //        return controller.Ending(context.HttpContext);
                //    default:
                //        return controller.Intro(context.HttpContext);
                //}
            //}
            var control = new Controllers.Marearcat();
            string url = context.HttpContext.Request.Path.Value.TrimEnd('/').ToLower();
            if (url.StartsWith("/marearcat/intro", StringComparison.OrdinalIgnoreCase))
            {
                context.Handler = async ctx =>
                {
                    ctx.Response.ContentType = "text/html;charset=utf-8";
                    //await ctx.Response.WriteAsync("Dapoova!");
                    await control.Intro(context.HttpContext);
                };
            }
            if (url.StartsWith("/marearcat/ending", StringComparison.OrdinalIgnoreCase))
            {
                context.Handler = async ctx =>
                {
                    ctx.Response.ContentType = "text/html;charset=utf-8";
                    //await ctx.Response.WriteAsync("Dapoova!");
                    await control.Ending(context.HttpContext);
                };
            }
            return Task.CompletedTask;
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class ForumActionResult : ActionResult
    {
        private readonly int requestCount;

        public ForumActionResult(int requestCount)
        {
            this.requestCount = requestCount;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add("ServerApplication", "Forum");
            return context.HttpContext.Response.WriteAsync(requestCount.ToString());
        }
    }
}
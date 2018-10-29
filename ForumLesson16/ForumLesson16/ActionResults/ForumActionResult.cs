using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class ForumActionResult : ActionResult
    {
        private readonly string message;

        public ForumActionResult(string message)
        {
            this.message = message;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add("ServerApplication", "Forum");
            return context.HttpContext.Response.WriteAsync(message);
        }
    }
}
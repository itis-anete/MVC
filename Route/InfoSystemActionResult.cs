using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Route
{
    public class InfoSystemActionResult : IActionResult
    {
        public string Name { get; set; }
        
        public new Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add("ServerApplication", "InfoSystem");
            return context.HttpContext.Response.WriteAsync("This is ISActionResult!");
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Route.Filters
{
    public class ActionFilter : Attribute, IActionFilter, IAsyncActionFilter
    {
        public async void OnActionExecuting(ActionExecutingContext context)
        {
            if ((DateTime.Now - Router.Start.Value).TotalMilliseconds > 1)
                await context.HttpContext.Response.WriteAsync("Request Timed Out.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.Result = new ContentResult {Content = "ActionFilter completed."};
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            OnActionExecuting(context);
            OnActionExecuted(next.Invoke().Result);
            return Task.CompletedTask;
        }
    }
}
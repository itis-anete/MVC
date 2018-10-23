using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Route
{
    public class ExceptionFilter : ExceptionFilterAttribute, IAsyncExceptionFilter, IExceptionFilter, IOrderedFilter
    {
        public new Task OnExceptionAsync(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            OnException(context);
            return Task.CompletedTask;
        }

        public new void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.WriteAsync(context.Exception.Message);
        }
    }
}
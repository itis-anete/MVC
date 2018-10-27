using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Route
{
    public class ExceptionFilter : Attribute, IAsyncExceptionFilter, IExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new ContentResult{Content = string.Join("", context.Exception.Message.Reverse())};
            context.Result.ExecuteResultAsync(context);
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }

        public async void OnException(ExceptionContext context)
        {
            await OnExceptionAsync(context);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class ReverseExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new ContentResult
            {
                Content = context.Exception.Message.Reverse().ToString()
            };
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
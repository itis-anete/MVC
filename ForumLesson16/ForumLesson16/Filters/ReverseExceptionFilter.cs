using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace ForumLesson16
{
    public class ReverseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // куда засовывать мэссэдж?
            context.Exception.Message.Reverse();
        }
    }
}

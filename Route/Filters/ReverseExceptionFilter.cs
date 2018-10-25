using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.Filters
{
    public class ReverseExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            IEnumerable<char> charExceptionMessage = context.Exception.Message.Reverse();
            string exceptionMessage = "";
            foreach (var ch in charExceptionMessage)
                exceptionMessage += ch;
            context.Result = new ContentResult { Content = exceptionMessage };
            context.ExceptionHandled = true;
        }
    }
}

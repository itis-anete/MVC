using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarketplaceMVC.Filters
{
    public class ReverseExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionMessage = string.Join("", context.Exception.Message.Reverse());

            context.Result = new ContentResult { Content = exceptionMessage };

            context.ExceptionHandled = true;
        }
    }
}

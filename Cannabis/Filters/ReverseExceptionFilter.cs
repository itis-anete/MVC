using Cannabis.ActionResults;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Cannabis.Filters
{
    public class ReverseExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.Result = RawResult.Create(
                new string(
                    context.Exception
                        .ToString()
                        .Reverse()
                        .ToArray()
                    )
                );
            context.ExceptionHandled = true;
        }
    }
}

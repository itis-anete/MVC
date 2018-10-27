using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Cannabis.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            throw new NotImplementedException();
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarketplaceMVC.Filters
{
    public class DelayActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var date = (DateTime.Now - Program.RequestTime.Value).TotalMilliseconds;
            if (date > 60)
                context.Result = new ContentResult { Content = "Слишком большая задержка" };
        }
    }
}

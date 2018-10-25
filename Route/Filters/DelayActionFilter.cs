using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.Filters
{
    public class DelayActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Program.GetProp().Value = DateTime.Now;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if ((DateTime.Now - Program.GetProp().Value).TotalMilliseconds > 60)
                context.Result = new ContentResult { Content = "Слишком большая задержка" };
        }
    }
}

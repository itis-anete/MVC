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
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            double date = (DateTime.Now - Program.GetProp().Value).TotalMilliseconds;
            if (date > 60)
                context.Result = new ContentResult { Content = "Слишком большая задержка" };
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class TimeActionFilter : IActionFilter
    {
        private DateTime traceStart;
        public readonly Stopwatch stopwatch;

        public TimeActionFilter()
        {
            stopwatch = new Stopwatch();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            traceStart = DateTime.UtcNow;
            stopwatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();
            var traceEnd = traceStart
                           .AddMilliseconds(stopwatch.ElapsedMilliseconds);
            if (traceEnd>1)
                    context.Result = new ContentResult { Content = "Слишком долго!" };
        }
    }
}

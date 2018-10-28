using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class TimeActionFilter : IActionFilter
    {
        private ThreadLocal<bool> isCancelled = new ThreadLocal<bool>() { Value = false };

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.Canceled = isCancelled.Value;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Timer.Stopwatch.Stop();
            if (Timer.Stopwatch.ElapsedMilliseconds > 500)
                isCancelled.Value = true;
        }
    }

    public static class Timer
    {
        public static Stopwatch Stopwatch => stopwatch.Value;
        private static readonly ThreadLocal<Stopwatch> stopwatch;
    }
}

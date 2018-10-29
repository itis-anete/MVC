using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Threading;

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
            isCancelled.Value = Timer.Stopwatch.ElapsedMilliseconds > 500;
        }
    }

    public static class Timer
    {
        public static Stopwatch Stopwatch => stopwatch.Value;
        private static readonly ThreadLocal<Stopwatch> stopwatch = 
            new ThreadLocal<Stopwatch>();
    }
}
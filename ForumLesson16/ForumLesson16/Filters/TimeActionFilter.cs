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
            if (context.Canceled)
            {
                context.HttpContext.Response.StatusCode = 408;
            }
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
        private static readonly ThreadLocal<Stopwatch> stopwatch;

        static Timer()
        {
            stopwatch = new ThreadLocal<Stopwatch>();
        }

        public static void StartNewTimer()
        {
            stopwatch.Value = Stopwatch.StartNew();
        }
    }
}
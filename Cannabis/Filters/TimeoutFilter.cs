using Cannabis.ActionResults;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace Cannabis.Filters
{
    public class TimeoutFilter : ActionFilterAttribute
    {
        public TimeSpan Timeout { get; set; }

        public TimeoutFilter() : this(DefaultTimeout) { }

        public TimeoutFilter(double timeoutInMilliseconds)
            : this(TimeSpan.FromMilliseconds(timeoutInMilliseconds)) { }

        public TimeoutFilter(TimeSpan timeout)
        {
            Timeout = timeout;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var currentDateTime = DateTime.Now;
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.RouteData.DataTokens.TryGetValue("ActionStartTime", out var dateTimeObject) &&
                dateTimeObject is DateTime startDateTime &&
                currentDateTime - startDateTime >= Timeout
            )
                context.Result = RawResult.Create("Timeout");
        }

        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromMilliseconds(60);
    }
}

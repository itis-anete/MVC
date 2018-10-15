using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Route.Filters
{
    public class ActionFilter : Attribute,
        IActionFilter,
        IAsyncActionFilter,
        IResultFilter,
        IAsyncResultFilter,
        IOrderedFilter
    {
        public int Order { get; } = 0;

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public virtual void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            var controller = context.Controller;
            switch (controller)
            {
                case null:
                    throw new InvalidOperationException(nameof(controller));
                case IAsyncActionFilter asyncActionFilter:
                    return asyncActionFilter.OnActionExecutionAsync(context, next);
                case IActionFilter actionFilter:
                    return ExecuteActionFilter(context, next, actionFilter);
                default:
                    return next();
            }
        }

        private static async Task ExecuteActionFilter(
            ActionExecutingContext context, 
            ActionExecutionDelegate next,
            IActionFilter actionFilter)
        {
            actionFilter.OnActionExecuting(context);
            if (context.Result == null)
            {
                actionFilter.OnActionExecuted(await next());
            }
        }
        public void OnResultExecuting(ResultExecutingContext context)
        {
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            OnResultExecuting(context);
            if (!context.Cancel)
            {
                OnResultExecuted(await next());
            }
        }
    }
}
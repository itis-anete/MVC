using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Cannabis.Filters
{
    public class ResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            throw new NotImplementedException();
        }
    }
}

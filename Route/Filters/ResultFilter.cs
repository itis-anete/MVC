using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Route.Filters
{
    public class ResultFilter : ResultFilterAttribute, IResultFilter
    {
        public new void OnResultExecuting(ResultExecutingContext context)
        {
            // HtmlAgilityPack - optional
            context.HttpContext.Response.WriteAsync("Result Filter");
        }

        public new void OnResultExecuted(ResultExecutedContext context)
        {
            context.HttpContext.Response.WriteAsync(context.Result.ToString());
        }
    }
}
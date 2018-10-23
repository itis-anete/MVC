using Microsoft.AspNetCore.Mvc.Filters;

namespace Route.Filters
{
    public class ResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            //context.HttpContext.Response
            throw new System.NotImplementedException();
            // HtmlAgilityPack - optional
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
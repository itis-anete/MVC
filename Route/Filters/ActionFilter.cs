using Microsoft.AspNetCore.Mvc.Filters;

namespace Route
{
    public class ActionFilter : IActionFilter
    {   
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Action Filter Attribute
            throw new System.NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Route.Filters
{
    public class ActionFilter : Attribute, IActionFilter
    {
        /// <summary>
        /// Description
        /// время запроса можно получить в роутере (маршрутизации)
        /// сравнивать время сейчас - время начала запроса 
        /// оптимальное время задержки - если больше 60мс => отказ в Response (не слать http ошибки, текстом)
        /// </summary>

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.HttpContext.Response.WriteAsync("Abort: Request timed out.");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
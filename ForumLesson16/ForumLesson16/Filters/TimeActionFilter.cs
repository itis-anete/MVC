using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class TimeActionFilter : Attribute, IActionFilter
    {
        DateTime start;
        double time;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            start = DateTime.Now;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            DateTime end = DateTime.Now;
            double processTime = end.Subtract(start).TotalMilliseconds;
            if (processTime > time)
            {
                //Создать фильтр на действие, который будет проверять сколько времени прошло с начала запроса 
                //до прихода в фильтр, если время больше чем предполагалось, фильтр должен не пропускать обработку????
                
            }
        }
    }
}

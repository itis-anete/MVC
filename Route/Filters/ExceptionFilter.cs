using Microsoft.AspNetCore.Mvc.Filters;

namespace Route
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
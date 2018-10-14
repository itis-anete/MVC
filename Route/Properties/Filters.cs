
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Route.Properties
{
    public class Filters
    {
        public IActionFilter ActionFilte { get; }
        public IExceptionFilter ExceptionFilter { get; }
        public IPageFilter PageFilter { get; }
        public IAuthorizationFilter AuthorizationFilter { get; }
        public IResultFilter ResultFilter { get; }

    }

    public interface IResultFilter
    {
        void OnResultExecuting(ResultExecutingContext filterContext);
        void OnResultExecuted(ResultExecutedContext filterContext);
    }

    public interface IAuthorizationFilter
    {
        Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
                   CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation);
    }

    public interface IPageFilter
    {
    }

    public interface IExceptionFilter
    {
        void OnException(ExceptionContext filterContext);
    }

    public interface IActionFilter
    {
        void OnActionExecuting(ActionExecutingContext filterContext);
        void OnActionExecuted(ActionExecutedContext filterContext);
    }

    public class IndexException : FilterAttribute, IExceptionFilter
    {

        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is IndexOutOfRangeException)
            {
                exceptionContext.Result = new RedirectResult("/Content/ExceptionFound.html");
                exceptionContext.ExceptionHandled = true;
            }
        }
    }

    public class FilterAttribute
    {
    }
}

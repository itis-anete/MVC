using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Cannabis.Routing
{
    public class ControllerActionInvokerProvider : IActionInvokerProvider
    {
        public int Order => 1;

        public void OnProvidersExecuted(ActionInvokerProviderContext context)
        {
        }

        public void OnProvidersExecuting(ActionInvokerProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var controllerContext = new ControllerContext(context.ActionContext)
            {
                ValueProviderFactories = new[] { new RouteValueProviderFactory() }
            };
            var controllerFactory = new ControllerFactory();

            context.Result = new ControllerActionInvoker(controllerContext, controllerFactory);
        }
    }
}

using Microsoft.AspNetCore.Mvc.Abstractions;
using System;

namespace Cannabis.Routing
{
    public class ActionInvokerProvider : IActionInvokerProvider
    {
        public int Order => 1;

        public void OnProvidersExecuted(ActionInvokerProviderContext context)
        {
        }

        public void OnProvidersExecuting(ActionInvokerProviderContext context)
        {
            context.Result = null;
            throw new NotImplementedException();
        }
    }
}

using Microsoft.AspNetCore.Mvc.Abstractions;
using System;

namespace Cannabis.Routing
{
    public class ActionDescriptorProvider : IActionDescriptorProvider
    {
        public int Order => 1;

        public void OnProvidersExecuted(ActionDescriptorProviderContext context)
        {
        }

        public void OnProvidersExecuting(ActionDescriptorProviderContext context)
        {
            throw new NotImplementedException();
        }
    }
}

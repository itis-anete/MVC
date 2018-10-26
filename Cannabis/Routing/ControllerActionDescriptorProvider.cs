using Cannabis.Controllers;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cannabis.Routing
{
    public class ControllerActionDescriptorProvider : IActionDescriptorProvider
    {
        public int Order => 1;

        public void OnProvidersExecuted(ActionDescriptorProviderContext context)
        {
        }

        public void OnProvidersExecuting(ActionDescriptorProviderContext context)
        {
            var result = new List<ControllerActionDescriptor>();
            foreach (var controller in Assembly.GetCallingAssembly()
                .GetTypes()
                .Where(type => typeof(ICannabisController).IsAssignableFrom(type)))
                foreach (var action in controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    var httpMethodAttribute = action.GetCustomAttribute<HttpMethodAttribute>(true);
                    var httpMethod = httpMethodAttribute?.HttpMethods.FirstOrDefault() ?? "GET";

                    var actionDescriptor = new ControllerActionDescriptor()
                    {
                        ActionName = httpMethod + action.Name,
                        DisplayName = action.Name,
                        ControllerName = controller.Name,
                        MethodInfo = action
                    };
                    actionDescriptor.RouteValues["HttpMethod"] = httpMethod;

                    context.Results.Add(actionDescriptor);
                }
        }
    }
}

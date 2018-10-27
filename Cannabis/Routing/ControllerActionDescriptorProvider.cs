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
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var result = new List<ControllerActionDescriptor>();
            foreach (var controller in ProjectInfo.ControllerTypes)
                foreach (var action in controller
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    var httpMethodAttribute = action.GetCustomAttribute<HttpMethodAttribute>(true);
                    var httpMethod = httpMethodAttribute?.HttpMethods.FirstOrDefault() ?? "GET";

                    var parameterDescriptors = action.GetParameters()
                        .Select(parameter => new ParameterDescriptor()
                            {
                                Name = parameter.Name,
                                ParameterType = parameter.ParameterType
                            })
                        .ToList();

                    var actionDescriptor = new ControllerActionDescriptor()
                    {
                        ActionName = httpMethod + action.Name,
                        DisplayName = action.Name,
                        ControllerName = controller.Name,
                        MethodInfo = action,
                        Parameters = parameterDescriptors
                    };

                    context.Results.Add(actionDescriptor);
                }
        }
    }
}

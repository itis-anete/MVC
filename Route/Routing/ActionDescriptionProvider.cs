using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Internal;

namespace Route
{
    public class ActionDescriptorProvider : IActionDescriptorProvider
    {
        public int Order { get; }
        private readonly IEnumerable<IApplicationModelConvention> _conventions;

        public void OnProvidersExecuting(ActionDescriptorProviderContext context)
        {
            var model = new ApplicationModelProviderContext(null).Result;
            ApplicationModelConventions.ApplyConventions(model, _conventions);
            foreach (var e in ControllerActionDescriptorBuilder.Build(model))
            {
                context.Results.Add(e);
            }
        }

        public void OnProvidersExecuted(ActionDescriptorProviderContext context)
        {
            var keys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var action in context.Results)
            foreach (var key in action.RouteValues.Keys)
                keys.Add(key);

            foreach (var action in context.Results)
            foreach (var key in keys)
                if (!action.RouteValues.ContainsKey(key))
                    action.RouteValues.Add(key, null);
        }
    }
}
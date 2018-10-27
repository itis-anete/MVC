using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cannabis.Routing
{
    public class ControllerActionSelector : IActionSelector
    {
        private readonly Dictionary<string, Dictionary<string, List<ControllerActionDescriptor>>> _actions;

        public ControllerActionSelector(IActionDescriptorProvider actionDescriptorProvider)
        {
            if (actionDescriptorProvider == null)
                throw new ArgumentNullException(nameof(actionDescriptorProvider));

            var providerContext = new ActionDescriptorProviderContext();
            actionDescriptorProvider.OnProvidersExecuting(providerContext);
            actionDescriptorProvider.OnProvidersExecuted(providerContext);

            _actions = providerContext.Results
                .Select(descriptor => descriptor as ControllerActionDescriptor)
                .Where(descriptor => descriptor != null)
                .GroupBy(descriptor => descriptor.ControllerName)
                .ToDictionary(group => group.Key,
                    group => group
                        .GroupBy(descriptor => descriptor.ActionName)
                        .ToDictionary(group2 => group2.Key, group2 => group2.ToList()));
        }

        public ActionDescriptor SelectBestCandidate(RouteContext context, IReadOnlyList<ActionDescriptor> candidates)
        {
            return candidates?.FirstOrDefault();
        }

        public IReadOnlyList<ActionDescriptor> SelectCandidates(RouteContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var result = new List<ActionDescriptor>();
            
            var segments = context.HttpContext.Request.Path.Value?
                .Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (segments == null ||
                segments.Length < 2 ||
                segments.Length > 3 ||
                segments.Length == 3 && !segments[2].StartsWith('?')
            )
                return result;

            var controllerNamePrefix = ProjectInfo.ProjectName + '_';
            if (!segments[0].StartsWith(controllerNamePrefix))
                return result;
            var controllerName = segments[0].Substring(controllerNamePrefix.Length);
            var actionName = segments[1];
            var methodName = context.HttpContext.Request.Method;
            if (!actionName.StartsWith(methodName))
                return result;

            if (_actions.TryGetValue(controllerName, out var controllerActions))
                if (controllerActions.TryGetValue(actionName, out var actions))
                    result.AddRange(actions);

            return result;
        }
    }
}

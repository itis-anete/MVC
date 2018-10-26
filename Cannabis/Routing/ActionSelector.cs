using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cannabis.Routing
{
    public class ActionSelector : IActionSelector
    {
        public ActionDescriptor SelectBestCandidate(RouteContext context, IReadOnlyList<ActionDescriptor> candidates)
        {
            return candidates.FirstOrDefault();
        }

        public IReadOnlyList<ActionDescriptor> SelectCandidates(RouteContext context)
        {
            var requestPath = context.HttpContext.Request.Path.Value;
            if (!string.IsNullOrEmpty(requestPath) && requestPath[0] == '/')
                requestPath = requestPath.Substring(1);

            var segments = requestPath.Split('/');

            return new List<ActionDescriptor>();
        }
    }
}

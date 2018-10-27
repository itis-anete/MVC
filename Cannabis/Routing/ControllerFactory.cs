using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;

namespace Cannabis.Routing
{
    public class ControllerFactory : IControllerFactory
    {
        public object CreateController(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var controllerName = context.ActionDescriptor.ControllerName;
            if (!_controllers.TryGetValue(controllerName, out var controller))
            {
                controller = context.ActionDescriptor.ControllerTypeInfo
                    .GetConstructor(new Type[0])?
                    .Invoke(new object[0]);
                if (controller != null)
                    _controllers[controllerName] = controller;
            }
            return controller;
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
        }

        private readonly Dictionary<string, object> _controllers = new Dictionary<string, object>();
    }
}

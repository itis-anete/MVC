using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cannabis.Routing
{
    public class ControllerFactory : IControllerFactory
    {
        private static readonly Dictionary<string, Type> _controllerTypes
            = ProjectInfo.ControllerTypes.ToDictionary(type => type.Name);
        private readonly Dictionary<string, object> _controllers
            = new Dictionary<string, object>();

        public object CreateController(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var controllerName = context.ActionDescriptor.ControllerName;
            if (!_controllers.TryGetValue(controllerName, out var controller))
            {
                if (!_controllerTypes.TryGetValue(controllerName, out var controllerType))
                    return null;

                controller = controllerType
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
    }
}

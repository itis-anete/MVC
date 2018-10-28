using System;
using System.Collections.Generic;
using System.Linq;
using Cannabis.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;

namespace Cannabis.Routing
{
    public class ControllerFactory : IControllerFactory
    {
        public ControllerFactory(IControllerActivator activator) : this(activator, null) { }

        public ControllerFactory(
            IControllerActivator activator,
            IEnumerable<IControllerPropertyActivator> propertyActivators)
        {
            _controllerActivator = activator ?? throw new ArgumentNullException(nameof(activator));
            _propertyActivators = propertyActivators?.ToArray() ?? new IControllerPropertyActivator[0];
        }

        public object CreateController(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var controllerName = context.ActionDescriptor.ControllerName;
            if (!_controllers.TryGetValue(controllerName, out var controller))
            {
                controller = _controllerActivator.Create(context) as ICannabisController;
                if (controller != null)
                    _controllers[controllerName] = controller;
            }

            if (controller != null)
            {
                foreach (var activator in _propertyActivators)
                    activator.Activate(context, controller);
                ++controller.CallCounter;
            }
            return controller;
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
        }

        private readonly IControllerActivator _controllerActivator;
        private readonly IControllerPropertyActivator[] _propertyActivators;
        private readonly Dictionary<string, ICannabisController> _controllers
            = new Dictionary<string, ICannabisController>();
    }
}
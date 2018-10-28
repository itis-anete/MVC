using System;
using System.Collections.Generic;
using System.Linq;
using MarketplaceMVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;

namespace MarketplaceMVC.Routing
{
    public class ControllerFactory : IControllerFactory
    {
        private readonly Dictionary<string, IMarketplaceController> _controllers = new Dictionary<string, IMarketplaceController>();

        public IControllerActivator ControllerActivator { get; }

        public IControllerPropertyActivator[] PropertyActivators { get; }

        public ControllerFactory(IControllerActivator activator) : this(activator, null) { }

        public ControllerFactory(IControllerActivator activator, IEnumerable<IControllerPropertyActivator> propertyActivators)
        {
            ControllerActivator = activator ?? throw new ArgumentNullException(nameof(activator));
            PropertyActivators = propertyActivators?.ToArray() ?? new IControllerPropertyActivator[0];
        }

        public object CreateController(ControllerContext context)
        {
            var controllerName = context.ActionDescriptor.ControllerName;

            if (!_controllers.TryGetValue(controllerName, out var controller))
            {
                controller = ControllerActivator.Create(context) as IMarketplaceController;
                if (controller != null)
                    _controllers[controllerName] = controller;
            }

            if (controller != null)
            {
                foreach (var activator in PropertyActivators)
                    activator.Activate(context, controller);

                ++controller.CallCounter;

                return controller;
            }

            return null;
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
            //AFK
        }
        
    }

}

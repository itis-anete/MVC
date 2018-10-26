using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;

namespace MarketplaceMVC.Routing
{
    public class ControllerFactory : IControllerFactory
    {
        private readonly IControllerActivator _controllerActivator;
        private readonly IControllerPropertyActivator[] _propertyActivators;

        public ControllerFactory(IControllerActivator activator) : this(activator,
            new List<IControllerPropertyActivator>())
        {
        }

        public ControllerFactory(IControllerActivator activator, IEnumerable<IControllerPropertyActivator> propertyActivators)
        {
            _controllerActivator = activator 
                                   ?? throw new ArgumentNullException(nameof(activator));
            _propertyActivators = propertyActivators?.ToArray() 
                                  ?? throw new ArgumentNullException(nameof(propertyActivators));
        }

        public object CreateController(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var controller = _controllerActivator.Create(context);

            foreach (var pa in _propertyActivators)
                pa.Activate(context, controller);

            return controller;
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
            //_controllerActivator.Release(context, controller);
        }
    }

}

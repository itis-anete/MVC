using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route
{
    public class ControllerFactory : IControllerFactory
    {
        private readonly IControllerActivator controllerActivator;
        private readonly IControllerPropertyActivator[] propertyActivators;

        public ControllerFactory(
            IControllerActivator controllerActivator,
            IEnumerable<IControllerPropertyActivator> propertyActivators)
        {
            this.controllerActivator = controllerActivator ?? throw new ArgumentNullException();
            this.propertyActivators = propertyActivators?.ToArray() ?? throw new ArgumentNullException();
        }

        public object CreateController(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException();
            if (context.ActionDescriptor == null) throw new ArgumentException();

            var controller = controllerActivator.Create(context);
            foreach (var propertyActivator in propertyActivators)
                propertyActivator.Activate(context, controller);
            return controller;
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
            if (context == null) throw new ArgumentNullException();
            if (controller == null) throw new ArgumentNullException();

            controllerActivator.Release(context, controller);
        }
    }
}

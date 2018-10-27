using System;
using System.Collections.Generic;
using MarketplaceMVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace MarketplaceMVC.Routing
{
    public class ControllerFactory : IControllerFactory
    {
        private readonly Dictionary<string, IMarketplaceController> _controllers = new Dictionary<string, IMarketplaceController>();

        public object CreateController(ControllerContext context)
        {
            var controllerName = context.ActionDescriptor.ControllerName;

            if (_controllers.TryGetValue(controllerName, out var controller))
                return controller;

            controller = context.ActionDescriptor.ControllerTypeInfo.GetConstructor(new Type[0])?.Invoke(new object[0]) as IMarketplaceController;

            if (controller != null)
                _controllers[controllerName] = controller;

            ++controller.CallCounter;

            return controller;
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
            //AFK
        }
        
    }

}

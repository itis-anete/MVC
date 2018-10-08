using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;
using System;

namespace Route
{
    public class ControllerActivator : IControllerActivator
    {
        private readonly ITypeActivatorCache typeActivatorCache;

        public ControllerActivator(ITypeActivatorCache typeActivatorCache)
        {
            this.typeActivatorCache = typeActivatorCache ?? throw new ArgumentNullException();
        }

        public object Create(ControllerContext controllerContext)
        {
            if (controllerContext == null) throw new ArgumentNullException();
            if (controllerContext.ActionDescriptor == null) throw new ArgumentException();

            var controllerTypeInfo = controllerContext.ActionDescriptor.ControllerTypeInfo;
            if (controllerTypeInfo == null) throw new ArgumentException();

            var serviceProvider = controllerContext.HttpContext.RequestServices;
            return typeActivatorCache.CreateInstance<object>(serviceProvider, controllerTypeInfo.AsType());
        }

        public void Release(ControllerContext context, object controller)
        {
            if (context == null) throw new ArgumentNullException();
            if (controller == null) throw new ArgumentNullException();
            
            if (controller is IDisposable disposable) disposable.Dispose();
        }
    }
}

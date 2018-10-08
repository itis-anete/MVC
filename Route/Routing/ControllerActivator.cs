using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;

namespace Route
{
    public class ControllerActivator : IControllerActivator
    {
        private readonly ITypeActivatorCache _typeActivatorCache;

        public ControllerActivator(ITypeActivatorCache typeActivatorCache)
        {
            _typeActivatorCache = typeActivatorCache ?? throw new ArgumentNullException(nameof(typeActivatorCache));
        }

        public object Create(ControllerContext controllerContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException(nameof(controllerContext));

            if (controllerContext.ActionDescriptor == null)
                throw new ArgumentException(nameof(controllerContext.ActionDescriptor));

            var controllerTypeInfo = controllerContext.ActionDescriptor.ControllerTypeInfo.AsType() ??
                                     throw new ArgumentNullException(nameof(controllerContext.ActionDescriptor
                                         .ControllerTypeInfo));

            var serviceProvider = controllerContext.HttpContext.RequestServices;
            return _typeActivatorCache.CreateInstance<object>(
                serviceProvider,
                controllerTypeInfo);
        }

        public void Release(ControllerContext context, object controller)
        {
            context.HttpContext.Response.WriteAsync("lol kek im release method in controller activator");
            if (controller is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Route
{
    public class ConrollerFactory : IControllerFactory
    {
        private readonly IControllerActivator _controllerActivator;

        public ConrollerFactory(IControllerActivator controllerActivator)
        {
            _controllerActivator = controllerActivator;
        }

        public object CreateController(ControllerContext context)
        {
            var controller = _controllerActivator.Create(context);
            return controller;
        }

        public void ReleaseController(ControllerContext context, object controller)
        {
            _controllerActivator.Release(context, controller);
        }
    }

    public class ControllerActivator : IControllerActivator
    {
        public object Create(ControllerContext context)
        {
            var controllerTypeInfo = context.ActionDescriptor.ControllerTypeInfo.AsType();

            var serviceProvider = context.HttpContext.RequestServices;

            return ActivatorUtilities.CreateInstance(serviceProvider, controllerTypeInfo);
        }

        public void Release(ControllerContext context, object controller)
        {
            if (controller is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

    public class ActionInvokerProvider : IActionInvokerProvider
    {
        public int Order => throw new NotImplementedException();

        public void OnProvidersExecuted(ActionInvokerProviderContext context)
        {
            throw new NotImplementedException();
        }

        public void OnProvidersExecuting(ActionInvokerProviderContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class ActionDescriptorProvider : IActionDescriptorProvider
    {
        public int Order => throw new NotImplementedException();

        public void OnProvidersExecuted(ActionDescriptorProviderContext context)
        {
            throw new NotImplementedException();
        }

        public void OnProvidersExecuting(ActionDescriptorProviderContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class ActionInvoker : IActionInvoker
    {
        public Task InvokeAsync()
        {
            throw new NotImplementedException();
        }
    }
}

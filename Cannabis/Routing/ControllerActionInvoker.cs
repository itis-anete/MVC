using Cannabis.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cannabis.Routing
{
    public class ControllerActionInvoker : IActionInvoker
    {
        public ControllerActionInvoker(
            ControllerContext controllerContext,
            ControllerFactory controllerFactory,
            ModelBinderProvider modelBinderProvider)
        {
            if (controllerContext == null)
                throw new ArgumentNullException(nameof(controllerContext));
            if (controllerFactory == null)
                throw new ArgumentNullException(nameof(controllerFactory));
            if (modelBinderProvider == null)
                throw new ArgumentNullException(nameof(modelBinderProvider));

            _controllerContext = controllerContext ?? throw new ArgumentNullException(nameof(controllerContext));
            _controller = controllerFactory.CreateController(controllerContext)
                ?? throw new ArgumentException($"{nameof(controllerFactory)} was smoking cannabis");
            _modelBinder = modelBinderProvider.GetBinder(null)
                ?? throw new ArgumentException($"{nameof(modelBinderProvider)} was smoking cannabis");
        }

        public async Task InvokeAsync()
        {
            var method = _controllerContext.ActionDescriptor.MethodInfo;

            var bindingContext = new DefaultModelBindingContext()
            {
                ActionContext = _controllerContext
            };
            await _modelBinder.BindModelAsync(bindingContext);
            
            var valueProvider = bindingContext.ValueProvider;
            throw new NotImplementedException();
        }

        private readonly object _controller;
        private readonly ControllerContext _controllerContext;
        private readonly IModelBinder _modelBinder;
    }
}

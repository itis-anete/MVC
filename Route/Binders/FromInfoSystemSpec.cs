using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Route.Models;

namespace Route
{
    public class FromInfoSystemSpecAttribute : ModelBinderAttribute, IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var model = new InfoSystemValueModel(new object());
            var parameters = bindingContext.ActionContext.ActionDescriptor.Parameters;
            var idValue = bindingContext.ActionContext.RouteData.Values["id"];
            if (idValue == null)
            {
                if (parameters.Count == 0)
                {
                    bindingContext.Model = model;
                    bindingContext.Result = ModelBindingResult.Success(model);
                    return Task.CompletedTask;
                }
                bindingContext.Result = ModelBindingResult.Failed();
                bindingContext.ModelState.AddModelError("id", "No parameter given?");
                return Task.CompletedTask;
            }
            model.Test = new InfoSystemValue(idValue);
            bindingContext.Model = model;
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
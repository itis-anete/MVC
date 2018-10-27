using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Route.Models;

namespace Route
{
    public class FromInfoSystemSpecAttribute : Attribute, IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var model = (InfoSystemValueModel) bindingContext.Model ?? new InfoSystemValueModel();
            var y = bindingContext.ActionContext.RouteData.Values["id"];
            if (y == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
            model.Prop = new InfoSystemValue(y);
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
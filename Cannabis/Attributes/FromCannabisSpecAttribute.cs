using Cannabis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cannabis.Attributes
{
    public class FromCannabisSpecAttribute : ModelBinderAttribute, IModelBinder
    {
        public FromCannabisSpecAttribute()
        {
            BinderType = GetType();
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            if (!typeof(CannabisValueModel).IsAssignableFrom(bindingContext.ModelType))
            {
                bindingContext.ModelState.AddModelError(
                    bindingContext.FieldName,
                    $"Type {bindingContext.ModelType} is not supported");
                return Task.CompletedTask;
            }
            
            var model = bindingContext.ModelType
                .GetConstructor(new Type[0])
                ?.Invoke(new object[0]);
            if (model == null)
            {
                bindingContext.ModelState.AddModelError(
                    bindingContext.FieldName,
                    $"Model {bindingContext.ModelType} is not supported");
                return Task.CompletedTask;
            }

            var parameters = bindingContext.HttpContext.Request.Query;
            foreach (var property in bindingContext.ModelType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(property =>
                    property.PropertyType == ValueType &&
                    property.SetMethod != null))
            {
                var value = parameters[property.Name].FirstOrDefault();
                if (value == null)
                    continue;
                property.SetValue(model, new CannabisValue(value));
            }

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }

        private static readonly Type ValueType = typeof(CannabisValue);
    }
}

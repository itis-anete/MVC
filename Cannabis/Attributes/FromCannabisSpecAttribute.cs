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
                .Invoke(new object[0]);

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

                var typeOfValue = property.GetCustomAttribute<CannabisValidValueAttribute>()?.ValidType
                    ?? typeof(string);
                var cannabisValue = ToCannabisValue(value, typeOfValue);
                if (cannabisValue == null)
                {
                    cannabisValue = new CannabisValue(value);
                    bindingContext.ModelState.AddModelError(property.Name, $"Can't parse \"{value}\" to type {typeOfValue}");
                }
                property.SetValue(model, cannabisValue);
            }

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }

        private static CannabisValue ToCannabisValue(string stringValue, Type type)
        {
            if (!Parsers.TryGetValue(type, out var parser))
                return null;

            var value = parser(stringValue);
            if (value == null)
                return null;

            return new CannabisValue(value);
        }

        private static readonly Type ValueType = typeof(CannabisValue);

        private static readonly Dictionary<Type, Func<string, object>> Parsers
            = new Dictionary<Type, Func<string, object>>
            {
                { typeof(bool), value => bool.TryParse(value, out var result) ? (object)result : null },
                { typeof(DateTime), value => DateTime.TryParse(value, out var result) ? (object)result : null },
                { typeof(double), value => double.TryParse(value, out var result) ? (object)result : null },
                { typeof(float), value => float.TryParse(value, out var result) ? (object)result : null },
                { typeof(int), value => int.TryParse(value, out var result) ? (object)result : null },
                { typeof(long), value => long.TryParse(value, out var result) ? (object)result : null },
                { typeof(string), value => value }
            };
    }
}

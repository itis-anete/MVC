using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MarketplaceMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarketplaceMVC.Attributes
{
    public class FromMarketplaceSpec : ModelBinderAttribute, IModelBinder
    {
        private static Type ValueType { get; } = typeof(MarketplaceValue);

        public FromMarketplaceSpec()
        {
            BinderType = GetType();
        }

        private static MarketplaceValue ToMarketplaceValue(string stringValue, Type type)
        {
            if (!Parsers.TryGetValue(type, out var parser))
                return null;

            var marketplaceValue = parser(stringValue);

            return marketplaceValue == null ? null : new MarketplaceValue(marketplaceValue);
        }

        private static readonly Dictionary<Type, Func<string, object>> Parsers
            = new Dictionary<Type, Func<string, object>>
            {
                { typeof(string), value => value },
                { typeof(DateTime), value => DateTime.TryParse(value, out var result) ? (object)result : null },
                { typeof(double), value => double.TryParse(value, out var result) ? (object)result : null },
                { typeof(float), value => float.TryParse(value, out var result) ? (object)result : null },
                { typeof(bool), value => bool.TryParse(value, out var result) ? (object)result : null },
                { typeof(int), value => int.TryParse(value, out var result) ? (object)result : null },
                { typeof(long), value => long.TryParse(value, out var result) ? (object)result : null },
            };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            if (!typeof(MarketplaceValueModel).IsAssignableFrom(bindingContext.ModelType))
            {
                bindingContext.ModelState.AddModelError(
                    bindingContext.FieldName,
                    $"Тип {bindingContext.ModelType} не поддерживается");
                return Task.CompletedTask;
            }
            
            var model = bindingContext.ModelType.GetConstructor(new Type[0]).Invoke(new object[0]);

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

                var typeOfValue = property.GetCustomAttribute<MarketplaceValidValueAttribute>()
                   ?.ValidType ?? typeof(string);

                var marketplaceValue = ToMarketplaceValue(value, typeOfValue);
                if (marketplaceValue == null)
                {
                    marketplaceValue = new MarketplaceValue(value);
                    bindingContext.ModelState
                        .AddModelError(property.Name, $"Невозможно привести \"{value}\" к {typeOfValue}");
                }
                property.SetValue(model, marketplaceValue);
            }

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}

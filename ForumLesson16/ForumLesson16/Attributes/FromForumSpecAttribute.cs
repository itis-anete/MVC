using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

namespace ForumLesson16
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromForumSpecAttribute : ModelBinderAttribute, IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var requestParameters = bindingContext.ActionContext.HttpContext.Request.Form;
            var modelType = bindingContext.ModelMetadata.ModelType;
            var model = Activator.CreateInstance(modelType);

            var properties = modelType
                .GetProperties()
                .Select(prop => Tuple.Create(prop, prop.GetCustomAttribute<ForumValidValueAttribute>()))
                .Where(tuple => tuple.Item1.PropertyType.GetType() == typeof(ForumValue) &&
                                tuple.Item2 != null);


            foreach (var tuple in properties)
            {
                if (requestParameters.TryGetValue(tuple.Item1.Name, out var value))
                {
                    var parameter = ParseParameter(value);
                    var forumValue = new ForumValue(parameter);
                    tuple.Item1.SetValue(model, forumValue);

                    if (forumValue.ValueType != tuple.Item1.PropertyType.GetType())
                        bindingContext.ModelState.AddModelError(tuple.Item1.Name, "Invalid type!");
                }
            }

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }

        private object ParseParameter(string parameter)
        {
            if (int.TryParse(parameter, out int intValue))
            {
                return intValue;
            }
            else if (long.TryParse(parameter, out long longValue))
            {
                return longValue;
            }
            else if (bool.TryParse(parameter, out bool boolValue))
            {
                return boolValue;
            }
            else if (DateTimeOffset.TryParse(parameter, out DateTimeOffset dateTimeOffset))
            {
                return dateTimeOffset;
            }
            return parameter;
        }
    }
}
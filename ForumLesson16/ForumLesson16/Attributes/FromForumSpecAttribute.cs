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
                .Where(prop => prop.GetCustomAttribute<ForumValidValueAttribute>() != null);

            foreach (var property in properties)
            {
                if (requestParameters.TryGetValue(property.Name, out var value))
                    property.SetValue(model, new ForumValue(value));
            }

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace ForumLesson16
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FromForumSpecAttribute : ModelBinderAttribute, IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var requestParameters = bindingContext.ActionContext.RouteData.Values;

            var model = new ForumModel();

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
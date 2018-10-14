using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Route.Validation
{
    public class ModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return new ModelBinder();
        }
    }
}

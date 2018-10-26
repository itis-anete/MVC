using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Cannabis.Validation
{
    public class ModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return new ModelBinder();
        }
    }
}

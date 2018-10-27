using Microsoft.AspNetCore.Mvc.ModelBinding;

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

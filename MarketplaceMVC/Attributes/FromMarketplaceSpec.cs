using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarketplaceMVC.Attributes
{
    public class FromMarketplaceSpec : Attribute, IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            throw new NotImplementedException();
        }
    }
}

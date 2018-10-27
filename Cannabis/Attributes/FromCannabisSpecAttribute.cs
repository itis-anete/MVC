using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cannabis.Attributes
{
    public class FromCannabisSpecAttribute : Attribute, IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));
            
            // достать параметры из bindingContext.HttpContext.Request.Query и
            // привязать их по bindingContext.ActionContext.ActionDescriptor.Parameters
            // к bindingContext.ValueProvider

            throw new NotImplementedException();
        }
    }
}

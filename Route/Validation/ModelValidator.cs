using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Route
{
    public class ModelValidator : IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var x = context.Model as ModelBinder.UserModel;
            //return new List<ModelValidationResult>().Add(new ModelValidationResult());
            throw new NotImplementedException();
        }
    }    
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Route
{
    public class ValidationStrategy : IValidationStrategy
    {
        public IEnumerator<ValidationEntry> GetChildren(ModelMetadata metadata, string key, object model)
        {
            throw new System.NotImplementedException();
        }
    }
}
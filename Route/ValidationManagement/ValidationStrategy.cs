using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.ValidationManagement
{
    public class ValidationStrategy : IValidationStrategy
    {
        public IEnumerator<ValidationEntry> GetChildren(ModelMetadata metadata, string key, object model)
        {
            throw new NotImplementedException();
        }
    }
}

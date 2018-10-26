using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cannabis.Models
{
    public class CannabisModel : CannabisValue, IValidatableObject
    {
        public CannabisModel(object value) : base(value) { }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

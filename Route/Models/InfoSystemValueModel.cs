using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Route.Models
{
    public class InfoSystemValueModel : InfoSystemValue, IValidatableObject
    {
        public InfoSystemValue Test { get; set; }

        public InfoSystemValueModel(object value) : base(value)
        {
            //Validate();
        }

        public InfoSystemValueModel()
        {
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var xx = validationContext.ObjectInstance as InfoSystemValueModel;
            foreach (var propertyInfo in typeof(InfoSystemValueModel).GetProperties())
            {
                var x = propertyInfo;
            }

            return new List<ValidationResult>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ForumLesson16
{
    public abstract class ForumValueModel : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetType()
                .GetProperties()
                .Select(prop => Tuple.Create(prop, prop.GetCustomAttribute<ForumValidValueAttribute>()))
                .Where(tuple => tuple.Item1.GetType() != typeof(ForumValue) &&
                                tuple.Item2 != null)
                .Select(tuple => new ValidationResult($"'{tuple.Item1.Name}' has invalid type."));
        }
    }
}
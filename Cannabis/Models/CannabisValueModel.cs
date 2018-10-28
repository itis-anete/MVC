using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Cannabis.Models
{
    public class CannabisValueModel : CannabisValue, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return fields.Select(field => ValidateMember(field, field.GetValue(this)))
                .Concat
                (
                    properties.Select(property => ValidateMember(property, property.GetValue(this)))
                )
                .Where(result => result != null);
        }

        private ValidationResult ValidateMember(MemberInfo member, object value)
        {
            var type = value?.GetType();
            if (type == null)
                return null;

            if (!ValueValidators.TryGetValue(type, out var validator))
                return null;

            var errorMessage = validator(value);
            return errorMessage == null ? null
                : new ValidationResult(errorMessage, new[] { member.Name });
        }

        private static readonly Dictionary<Type, Func<object, string>> ValueValidators
            = new Dictionary<Type, Func<object, string>>
            {
                { typeof(int),
                    value => (int)value >= -1 ? null
                         : "Number must be not smaller than -1" },
                { typeof(string),
                    value => ((string)value).Length >= 10 ? null
                        : "String must be not shorter than 10 symbols" }
            };
    }
}

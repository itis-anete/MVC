using Cannabis.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Cannabis.Models
{
    public class CannabisValueModel : CannabisValue, IValidatableObject
    {
        public CannabisValueModel() : base((object)null) { }
        public CannabisValueModel(object value) : base(value) { }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ValidationResult ValidateMember(MemberInfo member, object value)
            {
                if (member.GetCustomAttribute<CannabisValidValueAttribute>() != null)
                {
                    var errorMessage = IsValidValue(value);
                    if (errorMessage != null)
                        return new ValidationResult(errorMessage, new[] { member.Name });
                }
                return null;
            }

            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return fields.Select(field => ValidateMember(field, field.GetValue(this)))
                .Concat
                (
                    properties.Select(property => ValidateMember(property, property.GetValue(this)))
                )
                .Where(result => result != null);
        }

        private string IsValidValue(object value)
        {
            var type = value.GetType();
            if (type == typeof(int))
                return (int)value >= -1 ? null
                    : "Number must be not smaller than -1";

            if (type == typeof(string))
                return ((string)value).Length >= 10 ? null
                    : "String must be not shorter than 10 symbols";

            return null;
        }
    }
}

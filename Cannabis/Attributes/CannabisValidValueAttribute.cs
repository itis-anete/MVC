using Cannabis.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cannabis.Attributes
{
    public class CannabisValidValueAttribute : ValidationAttribute
    {
        public readonly Type ValidType;

        public CannabisValidValueAttribute() : this(null) { }
        public CannabisValidValueAttribute(Type validType)
        {
            ValidType = validType ?? typeof(string);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
                throw new ArgumentNullException(nameof(validationContext));

            if (value == null)
                return ValidationResult.Success;

            if (!(value is CannabisValue cannabisValue))
                return new ValidationResult($"Member must be of type {typeof(CannabisValue).Name}");

            var newCannabisValue = ToCannabisValue(cannabisValue.ToString(), ValidType);
            if (newCannabisValue == null)
                return new ValidationResult($"Can not parse \"{value}\" to type {ValidType}");

            cannabisValue.Value = newCannabisValue.Value;
            return ValidationResult.Success;
        }

        private static CannabisValue ToCannabisValue(string stringValue, Type type)
        {
            if (!Parsers.TryGetValue(type, out var parser))
                return null;

            var value = parser(stringValue);
            if (value == null)
                return null;

            return new CannabisValue(value);
        }

        private static readonly Dictionary<Type, Func<string, object>> Parsers
            = new Dictionary<Type, Func<string, object>>
            {
                { typeof(bool), value => bool.TryParse(value, out var result) ? (object)result : null },
                { typeof(DateTime), value => DateTime.TryParse(value, out var result) ? (object)result : null },
                { typeof(double), value => double.TryParse(value, out var result) ? (object)result : null },
                { typeof(float), value => float.TryParse(value, out var result) ? (object)result : null },
                { typeof(int), value => int.TryParse(value, out var result) ? (object)result : null },
                { typeof(long), value => long.TryParse(value, out var result) ? (object)result : null },
                { typeof(string), value => value }
            };
    }
}

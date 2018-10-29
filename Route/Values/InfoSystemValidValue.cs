using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Route.Models;

namespace Route
{
	public class InfoSystemValidValueAttribute :  ValidationAttribute
	{
        public InfoSystemValidValueAttribute() : this(null) { }
        public InfoSystemValidValueAttribute(Type validType)
        {
            _validType = validType ?? typeof(string);
        }

	    private readonly Type _validType;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
                throw new ArgumentNullException(nameof(validationContext));

            if (value == null)
                return ValidationResult.Success;

            if (!(value is InfoSystemValue infoSystemValue))
                return new ValidationResult($"Member must be of type {typeof(InfoSystemValue).Name}");

            var newCannabisValue = ToCannabisValue(infoSystemValue.ToString(), _validType);
            if (newCannabisValue == null)
                return new ValidationResult($"Can not parse \"{value}\" to type {_validType}");

            infoSystemValue.Value = newCannabisValue.Value;
            return ValidationResult.Success;
        }

        private static InfoSystemValue ToCannabisValue(string stringValue, Type type)
        {
            if (!Parsers.TryGetValue(type, out var parser))
                return null;

            var value = parser(stringValue);
            return value == null ? null : new InfoSystemValue(value);
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
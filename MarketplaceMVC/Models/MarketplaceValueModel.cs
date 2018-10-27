using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using MarketplaceMVC.Attributes;

namespace MarketplaceMVC.Models
{
    public class MarketplaceValueModel : MarketplaceValue, IValidatableObject
    {
        public MarketplaceValueModel(object value) : base(value) { }

        private ValidationResult ValidateMember(MemberInfo member, object value)
        {
            if (member.GetCustomAttribute<MarketplaceValidValueAttribute>() == null)
                return null;

            var errorMessage = IsMarketplaceValueValid(value, 200, 0);

            return errorMessage != null 
                ? new ValidationResult(errorMessage, new[] { member.Name }) 
                : null;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            
            var validationResults = fields.Select(field => ValidateMember(field, field.GetValue(this)));

            return validationResults.Concat(properties.Select(property => ValidateMember(property, property.GetValue(this)))).Where(x => x != null);
        }

        private static string IsMarketplaceValueValid(object value, int maxLength, int minValue)
        {
            switch (value)
            {
                case int i:
                    return i >= minValue
                        ? null
                        : $"Минимальное значение для числа: {minValue}";
                case string s:
                    return s.Length <= maxLength 
                        ? null
                        : $"Максимальная длина строки {maxLength} символов";
            }

            return null;
        }
    }
}

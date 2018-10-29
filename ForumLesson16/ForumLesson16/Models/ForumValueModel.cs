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
                .Where(prop => !HasPropertyValidValue(prop, validationContext.ObjectInstance))
                .Select(prop => new ValidationResult($"'{prop}' has invalid value."));
        }
        
        private bool HasPropertyValidValue(PropertyInfo propertyInfo, object instanceToValidate)
        {
            var forumModel = instanceToValidate as ForumModel;
            var attribute = propertyInfo.GetCustomAttribute<ForumValidValueAttribute>();

            if (propertyInfo.GetValue(forumModel) is ForumValue forumValue &&
                attribute?.ValidType == forumValue.ValueType)
            {
                var value = forumValue.Value;
                var type = forumValue.ValueType;

                var length = type.GetProperty("Length", BindingFlags.Public);
                var count = type.GetProperty("Count", BindingFlags.Public);

                if (value is IComparable comparable)
                {
                    return comparable.CompareTo(0) > 0 == attribute.IsPositive;
                }
                else if (length != null)
                {
                    return (int)length.GetValue(value) <= attribute.MaxLength;
                }
                else if (count != null)
                {
                    return (int)count.GetValue(value) <= attribute.MaxLength;
                }
                else if (value is DateTimeOffset dateTimeOffset)
                {
                    return dateTimeOffset.Offset == attribute.DateTimeOffset.Value.Offset;
                }
            }
            return true;
        }
    }
}
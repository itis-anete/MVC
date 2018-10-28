using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public abstract class ForumValueModel : IValidatableObject
    {
        //private readonly IEnumerable<Tuple<PropertyInfo, ForumValidValueAttribute>> propertyInfos;

        //public ForumValueModel()
        //{
        //    propertyInfos = GetType()
        //        .GetProperties()
        //        .Select(prop => Tuple.Create(prop, prop.GetCustomAttribute<ForumValidValueAttribute>()))
        //        .Where(tuple => tuple.Item1.GetType() == typeof(ForumValue) && tuple.Item2 != null);
        //}

        //public bool IsModelValid() =>
        //    propertyInfos.Any(prop => prop.Item1.Get)

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

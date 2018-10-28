using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16.Attributes
{
    public class ForumValueMode : IValidatableObject
    {
        int MaxLength;
        bool PositiveOrNegativeValue;//if true=positive if false negative
        TimeZoneInfo TimeZone;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();//Для этого класс модели должен реализовать интерфейс IValidatableObject:
        }
    }
}

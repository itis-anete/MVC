using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Route.Models
{
    public class InfoSystemValueModel : InfoSystemValue, IValidatableObject
    {
		//public InfoSystemValueModel(object value) : base(value)
		//{
		//}

		public InfoSystemValueModel()
		{
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
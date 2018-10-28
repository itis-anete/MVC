using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Route.Models
{
    public class InfoSystemValueModel : InfoSystemValue, IValidatableObject
    {
		//public InfoSystemValueModel(object value) : base(value)
		//{
		//}

<<<<<<< HEAD
        public InfoSystemValueModel(object value) : base(value)
        {
            //Validate();
        }

        public InfoSystemValueModel()
        {
        }
=======
		public InfoSystemValueModel()
		{
		}
>>>>>>> 7cc19700699a4614e83f057db70acbf0256ce254

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Route.Models
{
    public class ViewModel : InfoSystemValueModel
    {
		[InfoSystemValidValue(typeof(string), ErrorMessage = "string")]
        public string Name { get; set; }
        public List<KeyValue> Options { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			List<ValidationResult> errors = new List<ValidationResult>();

			if (string.IsNullOrWhiteSpace(Name))
			{
				errors.Add(new ValidationResult("Введите имя"));
			}
			if (Name.Length > 120)
			{
				errors.Add(new ValidationResult("Слишком большое имя"));
			}

			return errors;
		}
	}

    public class KeyValue : InfoSystemValueModel
    {
		[InfoSystemValidValue(typeof(string), ErrorMessage = "string")]
		public int Key { get; set; }
        public object Value { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			List<ValidationResult> errors = new List<ValidationResult>();

			if (Key < 0)
			{
				errors.Add(new ValidationResult("Неверный ключ"));
			}

			return errors;
		}
	}
}
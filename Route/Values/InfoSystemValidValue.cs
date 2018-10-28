using System;
using System.ComponentModel.DataAnnotations;
using Route.Models;

namespace Route
{
	//   public class InfoSystemValidValueAttribute : InfoSystemValueModel, ValidationAttribute
	//   {
	//       public InfoSystemValidValueAttribute(Type type)
	//       {

	//       }

	//	public override bool IsValid(object value)
	//	{
	//		return false;
	//	}
	//}
	public class InfoSystemValidValueAttribute :  ValidationAttribute
	{
		Type _type;
		public InfoSystemValidValueAttribute(Type type)
		{
			_type = type;
		}

		public override bool IsValid(object value)
		{
			if (_type.IsEquivalentTo(value.GetType())) return true;
			return false;
		}
	}
}
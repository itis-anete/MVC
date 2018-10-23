using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Route
{
    public class ModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            bindingContext.Result = ModelBindingResult.Success(bindingContext.ValueProvider.GetValue("d4n0n"));
            return Task.CompletedTask;
        }

        /*[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
        sealed class CellPhoneValidateAttribute : Attribute, IPropertyValidationFilter
        {
            // See the attribute guidelines at 
            //  http://go.microsoft.com/fwlink/?LinkId=85236
            public CellPhoneValidateAttribute()
            {
                throw new NotImplementedException();
            }

            public bool ShouldValidateEntry(ValidationEntry entry, ValidationEntry parentEntry)
            {
                //EmailAddressAttribute
                throw new NotImplementedException();
            }
        }

        public class UserModel : IValidatableObject
        {
            public string EmailAddress { get; set; }
            
            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                throw new NotImplementedException();
            }
        }*/
        
    }
}
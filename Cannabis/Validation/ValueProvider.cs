using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Route.Validation
{
    public class ValueProvider : IValueProvider
    {
        public bool ContainsPrefix(string prefix)
        {
            return prefix.Contains("WTF");
        }

        public ValueProviderResult GetValue(string key)
        {
            return new ValueProviderResult(new Microsoft.Extensions.Primitives.StringValues("Help"));
        }
    }
}

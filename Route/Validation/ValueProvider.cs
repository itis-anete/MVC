using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Route
{
    public class ValueProvider : IValueProvider
    {
        public string stringValue { get; }
        
        public bool ContainsPrefix(string prefix)
        {
            return stringValue.Contains(prefix);
        }

        public ValueProviderResult GetValue(string key) // key ?
        {
            return new ValueProviderResult(new[] {key});
        }
    }
}
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
            var x = new Str(1);
            return new ValueProviderResult(new[] {key});
        }
        
        
    }
    
    class Str
    {
        public Str(int a)
        {
            Length = a;
        }
        public int Length;
        public void Clear()
        {
            
        }
    }
}
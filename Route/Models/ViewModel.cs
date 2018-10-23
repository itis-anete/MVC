using System.Collections.Generic;

namespace Route.Models
{
    public class ViewModel
    {
        public string Name { get; set; }
        public List<KeyValue> Options { get; set; }
    }

    public class KeyValue
    {
        public int Key { get; set; }
        public object Value { get; set; }
    }
}
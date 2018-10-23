using Microsoft.Extensions.Primitives;

namespace Route
{
    public class InfoSystemValue
    {
        public InfoSystemValue(object value)
        {
            if (value.GetType() == typeof(string))
                stringValue = value.ToString();
        }
        // все значения

        private long? intValue;
        private float floatValue;
        private object objValue;
        private bool? boolValue;
        private string stringValue;
        //...

        public void GetType()
        {
            
        }

        public void GetValue()
        {
            
        }
        
    }

    public class Model : InfoSystemValue
    {
        [InfoSystemValidValue(typeof(string))]
        public InfoSystemValue Name { get; set; }

        public Model(object value) : base(value)
        {
            typeof(InfoSystemValue).IsAssignableFrom(typeof(SubModel)); // Submodel типа InfoSystemValue
        }
    }
    
    public class SubModel : InfoSystemValue
    {
        public SubModel(object value) : base(value)
        {
        }
    }
}
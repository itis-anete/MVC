using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceMVC.Models
{
    public class MarketplaceValue
    {
        public bool BoolValue { get; }
        public long IntValue { get; }
        public object ObjectValue { get; }
        public string StringValue { get; }
        public double DoubleValue { get; set; }

        public Type TypeOfValue { get; set; }

        public object Value => 
            TypeOfValue == typeof(bool)
                ? BoolValue
                : TypeOfValue == typeof(int)
                    ? (int)IntValue
                    : (TypeOfValue == typeof(string)
                        ? StringValue
                        : ObjectValue);

        public MarketplaceValue(bool value)
        {
            BoolValue = value;
            TypeOfValue = value.GetType();
        }

        public MarketplaceValue(double value)
        {
            DoubleValue = value;
            TypeOfValue = value.GetType();
        }

        public MarketplaceValue(int value)
        {
            IntValue = value;
            TypeOfValue = value.GetType();
        }

        public MarketplaceValue(string value)
        {
            StringValue = value;
            TypeOfValue = value.GetType();
        }

        public MarketplaceValue(object value)
        {
            ObjectValue = value;
            TypeOfValue = value.GetType();
        }
    }
}

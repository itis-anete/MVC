using System;

namespace MarketplaceMVC.Models
{
    public class MarketplaceValue
    {
        public bool? BoolValue { get; }
        public long? IntValue { get; }
        public object ObjectValue { get; }
        public string StringValue { get; }
        public double? DoubleValue { get; set; }

        public Type MValueType { get; set; }

        public object MValue => 
            MValueType == typeof(bool)
                ? BoolValue
                : MValueType == typeof(int)
                    ? (int)IntValue
                    : MValueType == typeof (double) 
                        ? DoubleValue 
                        : MValueType == typeof(string)
                            ? StringValue
                            : ObjectValue;

        public MarketplaceValue(bool value)
        {
            BoolValue = value;
            MValueType = value.GetType();
        }

        public MarketplaceValue(int value)
        {
            IntValue = value;
            MValueType = value.GetType();
        }

        public MarketplaceValue(double value)
        {
            DoubleValue = value;
            MValueType = value.GetType();
        }

        public MarketplaceValue(string value)
        {
            StringValue = value;
            MValueType = value.GetType();
        }

        public MarketplaceValue(object value)
        {
            ObjectValue = value;
            MValueType = value.GetType();
        }

        public override string ToString()
        {
            return MValue.ToString();
        }
    }
}

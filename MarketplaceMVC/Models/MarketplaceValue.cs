using System;

namespace MarketplaceMVC.Models
{
    public class MarketplaceValue
    {
        public bool? BoolValue { get; private set; }
        public long? IntValue { get; private set; }
        public double? DoubleValue { get; private set; }
        public object ObjectValue { get; private set; }
        public string StringValue { get; private set; }

        public Type MValueType { get; private set; }

        public object MValue
        {
            get => MValueType == typeof(bool)
                ? BoolValue
                : MValueType == typeof(int)
                    ? (int) IntValue
                    : MValueType == typeof(double)
                        ? DoubleValue
                        : MValueType == typeof(string)
                            ? StringValue
                            : ObjectValue;
            set
            {
                switch (value)
                {
                    case bool boolValue:
                        BoolValue = boolValue;
                        break;
                    case double doubleValue:
                        DoubleValue = doubleValue;
                        break;
                    case int intValue:
                        IntValue = intValue;
                        break;
                    case string stringValue:
                        StringValue = stringValue;
                        break;
                    default:
                        ObjectValue = value;
                        break;
                }
                MValueType = value?.GetType();
            }
        }

        public MarketplaceValue() : this(null)
        {
        }

        public MarketplaceValue(object value)
        {
            MValue = value;
        }

        public override string ToString() => MValue?.ToString() ?? "";
    }
}

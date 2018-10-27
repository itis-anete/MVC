using System;
using Microsoft.AspNetCore.Mvc;

namespace Route
{
    [ModelBinder(typeof(FromInfoSystemSpecAttribute))]
    public class InfoSystemValue
    {
        public InfoSystemValue()
        {   
        }
        
        public InfoSystemValue(object value)
        {
            if (int.TryParse(value.ToString(), out var intRes))
            {
                intValue = intRes;
                return;
            }

            if (bool.TryParse(value.ToString(), out var boolRes))
            {
                boolValue = boolRes;
                return;
            }

            if (double.TryParse(value.ToString(), out var doubleRes))
            {
                doubleValue = doubleRes;
                return;
            }
            
            if (value is string)
                stringValue = value.ToString();
            else
                objValue = value;
        }

        public InfoSystemValue(string value)
        {
            if (int.TryParse(value, out var intRes))
            {
                intValue = intRes;
                return;
            }

            if (bool.TryParse(value, out var boolRes))
            {
                boolValue = boolRes;
                return;
            }

            if (double.TryParse(value, out var doubleRes))
            {
                doubleValue = doubleRes;
                return;
            }

            stringValue = value;
        }

        public InfoSystemValue(long value)
        {
            intValue = value;
        }

        public InfoSystemValue(int value) : this((long) value)
        {
        }

        public InfoSystemValue(bool value)
        {
            boolValue = value;
        }

        public InfoSystemValue(float value) : this((double) value)
        {
        }

        public InfoSystemValue(double value)
        {
            doubleValue = value;
        }

        private long? intValue;
        private double? doubleValue;
        private object objValue;
        private bool? boolValue;
        private string stringValue;

        public new Type GetType()
        {
            var variables = new[] {intValue, doubleValue, objValue, boolValue, stringValue};
            foreach (var obj in variables)
                if (obj != null)
                    return obj.GetType();

            throw new InvalidOperationException("Cannot determine value type");
        }

        public object GetValue()
        {
            var variables = new[] {intValue, doubleValue, objValue, boolValue, stringValue};
            foreach (var obj in variables)
                if (obj != null)
                    return obj.GetType();

            throw new InvalidOperationException("No value");
        }

        public Type Type => intValue != null
            ? typeof(int)
            : boolValue != null
                ? typeof(bool)
                : doubleValue != null
                    ? typeof(double)
                    : stringValue != null
                        ? typeof(string)
                        : typeof(object);

        public object Value => objValue is int
            ? long.Parse(objValue.ToString())
            : objValue is double
                ? double.Parse(objValue.ToString())
                : objValue is bool
                    ? bool.Parse(objValue.ToString())
                    : objValue is string
                        ? objValue.ToString()
                        : objValue;
    }
}
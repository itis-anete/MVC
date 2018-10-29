using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public object Value
        {
            get
            {
                var variables = new[] {intValue, doubleValue, objValue, boolValue, stringValue};
                return (from obj in variables where obj != null select obj.GetType()).FirstOrDefault(); 
            }
            set
            {
                switch (value)
                {
                        case int iValue:
                            intValue = iValue;
                            break;
                        case bool bValue :
                            boolValue = bValue;
                            break;
                        case double dValue:
                            doubleValue = dValue;
                            break;
                        case string sValue:
                            stringValue = sValue;
                            break;
                        default:
                            objValue = value;
                            break;
                }
            }
        }
        private long? intValue;
        private double? doubleValue;
        private object objValue;
        private bool? boolValue;
        private string stringValue;

        public new Type GetType()
        {
            var variables = new[] {intValue, doubleValue, objValue, boolValue, stringValue};
            return (from obj in variables where obj != null select obj.GetType()).FirstOrDefault();
        }
    }
}
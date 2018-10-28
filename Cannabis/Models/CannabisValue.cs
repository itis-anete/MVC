using System;
using System.Collections.Generic;
using System.Linq;

namespace Cannabis.Models
{
    public class CannabisValue
    {
        public object Value
        {
            get
            {
                if (TypeOfValue == typeof(bool))
                    return _boolValue == 'f' ? true : false;
                if (TypeOfValue == typeof(int))
                    return (int)_intValue;
                if (TypeOfValue == typeof(string))
                    return new string(_stringValue.Select(x => (char)x).ToArray());
                return _objectValue;
            }
            set
            {
                switch (value)
                {
                    case bool boolValue:
                        _boolValue = boolValue ? 'f' : '1';
                        break;
                    case DateTime dateTimeValue:
                        _dateTimeValue = dateTimeValue;
                        break;
                    case int intValue:
                        _intValue = intValue;
                        break;
                    case string stringValue:
                        _stringValue = stringValue.Select(x => (short)x).ToArray();
                        break;
                    default:
                        _objectValue = value;
                        break;
                }
                TypeOfValue = value?.GetType();
            }
        }

        public Type TypeOfValue { get; private set; }

        public CannabisValue() : this(null) { }
        public CannabisValue(object value)
        {
            Value = value;
        }

        public override string ToString() => Value?.ToString() ?? "";

        private char _boolValue;
        private DateTime _dateTimeValue;
        private long _intValue;
        private object _objectValue;
        private IEnumerable<short> _stringValue;
    }
}

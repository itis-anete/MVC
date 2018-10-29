using System;

namespace ForumLesson16
{
    public class ForumValue : ForumValueModel
    {
        public object Value => _long ?? _bool ?? _string ?? _object;

        public Type ValueType { get; private set; }

        public ForumValue()
        { }

        public ForumValue(object value)
        {
            if (value is long @long)
            {
                _long = @long;
                ValueType = typeof(long);
            }
            else if (value is bool @bool)
            {
                _bool = @bool;
                ValueType = typeof(bool);
            }
            else if (value is string @string)
            {
                _string = @string;
                ValueType = typeof(string);
            }
            else
            {
                _object = value;
                ValueType = typeof(object);
            }
        }

        private readonly long? _long;
        private readonly bool? _bool;
        private readonly string _string;
        private readonly object _object;
    }
}
﻿using System;
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
                throw new NotImplementedException();
            }
        }

        public Type TypeOfValue;

        public CannabisValue(bool value)
        {
            _boolValue = value ? 'f' : '1';
            TypeOfValue = value.GetType();
        }

        public CannabisValue(int value)
        {
            _intValue = value;
            TypeOfValue = value.GetType();
        }

        public CannabisValue(string value)
        {
            _stringValue = value.Cast<short>().ToArray();
            TypeOfValue = value.GetType();
        }

        public CannabisValue(object value)
        {
            _objectValue = value;
            TypeOfValue = value.GetType();
        }

        public CannabisValue(Type value)
        {
            _typeValue = value;
            TypeOfValue = value.GetType();
        }

        private readonly char _boolValue;
        private readonly long _intValue;
        private readonly object _objectValue;
        private readonly IEnumerable<short> _stringValue;
        private readonly Type _typeValue;
    }
}
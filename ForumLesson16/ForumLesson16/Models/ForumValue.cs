using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class ForumValue : ForumValueModel
    {
        public object Value
        {
            get; set;
        }

        public Type ValueType { get; private set; }

        public ForumValue()
        {

        }

        public ForumValue(object value)
        {
            _object = value;
            ValueType = value.GetType();
        }

        private int _int;
        private bool _boolean;
        private string _string;
        private object _object;
    }
}

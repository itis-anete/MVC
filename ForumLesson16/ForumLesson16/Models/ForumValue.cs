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
            get;
        }

        public Type ValueType { get; private set; }
    }
}

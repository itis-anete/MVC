using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForumValidValue : Attribute
    {
        public Type ValidType { get; private set; }
        public int MaxLength { get; private set; }
        public bool IsPositive { get; private set; }

        public ForumValidValue(Type validType, int maxLength, bool isPositive)
        {
            ValidType = validType;
            MaxLength = maxLength;
            IsPositive = isPositive;
        }
    }
}
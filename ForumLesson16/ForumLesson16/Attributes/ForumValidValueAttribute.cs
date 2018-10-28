using System;

namespace ForumLesson16
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForumValidValueAttribute : Attribute
    {
        public Type ValidType { get; private set; }
        public int MaxLength { get; private set; }
        public bool IsPositive { get; private set; }
        public DateTimeOffset Offset { get; private set; }

        public ForumValidValueAttribute()
        {

        }

        public ForumValidValueAttribute(Type validType, int maxLength, bool isPositive)
        {
            ValidType = validType;
            MaxLength = maxLength;
            IsPositive = isPositive;
        }
    }
}
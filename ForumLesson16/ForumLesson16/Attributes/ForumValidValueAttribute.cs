using System;

namespace ForumLesson16
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForumValidValueAttribute : Attribute
    {
        public Type ValidType { get; private set; }
        public int? MaxLength { get; private set; }
        public bool? IsPositive { get; private set; }
        public DateTimeOffset? DateTimeOffset { get; private set; }

        public ForumValidValueAttribute(Type validType, bool isPositive)
        {
            ValidType = validType;
            IsPositive = isPositive;
        }

        public ForumValidValueAttribute(Type validType, int maxLength)
        {
            ValidType = validType;
            MaxLength = maxLength;
        }

        public ForumValidValueAttribute(Type validType, DateTimeOffset dateTimeOffset)
        {
            ValidType = validType;
            DateTimeOffset = dateTimeOffset;
        }

        public ForumValidValueAttribute(Type validType, int maxLength, bool isPositive)
        {
            ValidType = validType;
            MaxLength = maxLength;
            IsPositive = isPositive;
        }
    }
}
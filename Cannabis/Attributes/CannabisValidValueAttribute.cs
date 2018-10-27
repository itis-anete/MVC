using System;

namespace Cannabis.Attributes
{
    public class CannabisValidValueAttribute : Attribute
    {
        public readonly Type ValidType;

        public CannabisValidValueAttribute(Type validType)
        {
            ValidType = validType;
        }
    }
}

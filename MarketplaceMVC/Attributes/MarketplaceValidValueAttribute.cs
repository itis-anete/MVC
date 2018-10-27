using System;

namespace MarketplaceMVC.Attributes
{
    public class MarketplaceValidValueAttribute : Attribute
    {
        public Type ValidType { get; set; }

        public MarketplaceValidValueAttribute(Type validType)
        {
            ValidType = validType;
        }
    }
}
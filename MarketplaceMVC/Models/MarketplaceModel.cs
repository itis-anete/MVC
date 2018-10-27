using MarketplaceMVC.Attributes;

namespace MarketplaceMVC.Models
{
    public class MarketplaceModel : MarketplaceValueModel
    {
        [MarketplaceValidValue(typeof(int))]
        public MarketplaceValue Age { get; set; }
    }
}

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MarketplaceMVC.Models;

namespace MarketplaceMVC.Tags
{
    public static class MarketplaceButtonHelper
    {
        public static HtmlString MarketplaceButton(this IHtmlHelper html, MarketplaceValue value)
        {
            var result = "<button>";
            result += value.MValueType;
            result += "</button>";
            return new HtmlString(result);
        }
    }
}

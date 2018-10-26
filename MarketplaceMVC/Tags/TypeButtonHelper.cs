using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MarketplaceMVC.Models;

namespace MarketplaceMVC.Tags
{
    public static class TypeButtonHelper
    {
        public static HtmlString TypeButton(this IHtmlHelper html, MarketplaceValue value)
        {
            var result = "<button>";
            result += value.TypeOfValue;
            result += "</button>";
            return new HtmlString(result);
        }
    }
}

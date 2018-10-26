using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketplaceMVC.Tags
{
    public static class TypeButtonHelper
    {
        public static HtmlString TypeButton(this IHtmlHelper html, object dynamic)
        {
            var result = "<button>";
            result += dynamic.GetType();
            result += "</button>";
            return new HtmlString(result);
        }
    }
}

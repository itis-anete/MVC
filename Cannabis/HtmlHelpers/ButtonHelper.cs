using Cannabis.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cannabis.HtmlHelpers
{
    public static class ButtonHelper
    {
        public static HtmlString CannabisButton(this IHtmlHelper html, CannabisValue value)
        {
            return new HtmlString($"<button>{value.Value}</button>");
        }
    }
}

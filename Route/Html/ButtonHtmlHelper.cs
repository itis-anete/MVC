using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Route
{
    public static class ButtonHtmlHelper
    {
        public static IHtmlContent GetButton(this IHtmlHelper html, InfoSystemValue value)
        {
            var tag = new TagBuilder("button");
            tag.InnerHtml.AppendHtml(value.GetType().ToString());
            return tag;
        }
    }
}
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForumLesson16
{
    public static class ButtonNameHtmlHelper
    {
        public static HtmlString CannabisButton(this IHtmlHelper html, ForumValue value) =>
            new HtmlString($"<button>{value.Value}</button>");
    }
}

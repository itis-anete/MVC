using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Route
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent ListFor<TModel, TListItem>(this IHtmlHelper<TModel> helper,
            IEnumerable<TListItem> list, Func<TListItem, string> converter)
        {
            var contentBuilder = new HtmlContentBuilder();
            var tag = new TagBuilder("ul");
            contentBuilder.AppendLine("<ul>");

            foreach (var item in list)
            {
                contentBuilder.AppendLine("<li>");
                contentBuilder.AppendLine(converter(item));
                contentBuilder.AppendLine("</li>");

            }

            contentBuilder.AppendLine("</ul>");
            tag.InnerHtml.AppendHtml(contentBuilder);
            return tag;
        }
    }
}
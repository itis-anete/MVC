using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Route
{
    public class MyTableTag : TagHelper, ITagHelper
    {
        public new Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.Content.AppendHtml("<tr>");
            output.Content.AppendHtml("<th>First |</th>");
            output.Content.AppendHtml("<th>| Second |</th>");
            output.Content.AppendHtml("<th>| Third </th>");
            output.Content.AppendHtml("</tr>");
            AppendTableRow(output, new[] {1, 2, 3});
            AppendTableRow(output, new[] {4, 5, 6});
            return Task.CompletedTask;
        }

        private static void AppendTableRow(TagHelperOutput output, IEnumerable<int> numbers)
        {
            output.Content.AppendHtml("<tr>");
            foreach (var number in numbers)
                output.Content.AppendHtml($"<td>{number}</td>");
            output.Content.AppendHtml("</tr>");
        }
    }
}
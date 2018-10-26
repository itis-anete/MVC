using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Route
{
    public class ServerUl : ITagHelper
    {
        public string Value { get; set; }

        public void Init(TagHelperContext context)
        {
        }

        public Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Content.AppendHtml("<li>1</li>");
            output.Content.AppendHtml("<li>2</li>");
            output.Content.AppendHtml("<li>3</li>");
            output.Content.AppendHtml($"<li>{Value}</li>");
            return Task.CompletedTask;
        }

        public int Order => int.MinValue;
    }
}
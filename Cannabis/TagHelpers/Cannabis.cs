using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Cannabis.TagHelpers
{
    public class Cannabis : TagHelper
    {
        public string Link { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", Link);
            output.Content.SetContent("Some cool text");
        }
    }
}

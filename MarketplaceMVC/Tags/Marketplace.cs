using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MarketplaceMVC.Tags
{
    public class Marketplace : TagHelper
    {
        public string Link { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", Link);
            output.Content.SetContent("Следи за нами в github");
        }
    }
}

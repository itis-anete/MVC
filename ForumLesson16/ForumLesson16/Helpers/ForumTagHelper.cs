using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ForumLesson16.Helpers
{
    public class ForumTagHelper : TagHelper
    {
        public string ForumName { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "forum";
            output.Attributes.SetAttribute("name", ForumName);
            output.Content.SetContent("Original idea");
        }
    }
}
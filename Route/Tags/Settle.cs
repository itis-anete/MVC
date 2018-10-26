using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.Tags
{
    public class Settle : TagHelper
    {
        public string Address { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", Address);
            output.Content.SetContent("Следи за нами в github");
        }
    }
}

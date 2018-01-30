using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FreelancerBlog.Experimental.TagHelpers
{
    [HtmlTargetElement("formbutton")]
    public class FormButtonTagHelper : TagHelper
    {
        public string Type { get; set; } = "Submit";
        public string BgColor { get; set; } = "primary";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"btn btn-{BgColor}");
            output.Attributes.SetAttribute("type", Type);
            output.Content.SetContent(Type == "submit" ? "Add" : "Reset");
        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Socios.Web.Common.TagHelpers;

public abstract class AbstractBackLinkTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        output.Attributes.SetAttribute("href", "#");
        output.Attributes.SetAttribute("class", "btn btn-link btn-sm");
        output.Attributes.SetAttribute("onclick", "navigateBack();");
        base.Process(context, output);
    }
}

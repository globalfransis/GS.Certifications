using GSFSharedResources;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Common.TagHelpers;

public class GsfBackButtonTagHelper : TagHelper
{
    private readonly IStringLocalizer<Shared> _loc;

    public GsfBackButtonTagHelper(IStringLocalizer<Shared> loc)
    {
        _loc = loc;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);

        output.TagName = "a";
        output.Attributes.SetAttribute("href", "#");
        output.Attributes.SetAttribute("class", "btn btn-primary text-white btn-sm");
        output.Attributes.SetAttribute("onclick", "navigateBack();");

        output.PreContent.SetHtmlContent("<i class=\"fas fa-home mr-2\"></i>");

        var contentText = (await output.GetChildContentAsync()).GetContent();

        //if (string.IsNullOrWhiteSpace(contentText))
        //{
        //    output.Content.SetContent(_loc["Volver"]);
        //}

    }
}


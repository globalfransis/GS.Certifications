using GSFSharedResources;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Common.TagHelpers;

public class GsfBackLinkTagHelper : AbstractBackLinkTagHelper
{
    private readonly IStringLocalizer<Shared> _loc;

    public GsfBackLinkTagHelper(IStringLocalizer<Shared> loc)
    {
        _loc = loc;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);

        var content = await output.GetChildContentAsync();
        if (string.IsNullOrWhiteSpace(content.GetContent()))
        {
            output.Content.SetContent(_loc["Volver"]);
        }
    }

}

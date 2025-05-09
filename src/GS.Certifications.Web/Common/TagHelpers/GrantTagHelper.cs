using GSF.Application.Security.Grants.Queries;
using MediatR;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Common.TagHelpers;

public class GrantTagHelper : TagHelper
{
    public string GrantName { get; set; }
    private readonly IMediator _mediator;

    public GrantTagHelper(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = null;
        var queryGrant = new GetGrantsTempByNameQuery() { GrantName = GrantName };
        bool exist = await _mediator.Send(queryGrant);

        if (!exist) output.SuppressOutput();

    }
}

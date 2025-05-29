using GS.Certifications.Web.Common.Services;
using GSF.Application.Security.Options.GetOptions.Queries;
using GSF.Application.Security.Options.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

public class SideBarViewComponent : ViewComponent
{
    public IHttpContextAccessor _httpContextAccessor { get; }

    private readonly IArgumentFowardingService _argumentFowardingService;
    public OptionDto Options { get; set; }

    public OptionDto ContextOptions { get; set; }

    public bool HasContextMenu => ContextOptions.Options?.Any() ?? false;

    public string CurrentPage { get; set; }

    public SideBarViewComponent(IHttpContextAccessor httpContextAccessor, IArgumentFowardingService argumentFowardingService)
    {
        _httpContextAccessor = httpContextAccessor;
        _argumentFowardingService = argumentFowardingService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        CurrentPage = _httpContextAccessor.HttpContext.Items["navigationKey"].ToString();

        IMediator _mediator = (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));


        var queryContextOptions = new GetContextOptionsByPageQuery
        {
            Page = CurrentPage
        };
        OptionDto rootContextOptions = await _mediator.Send(queryContextOptions);
        foreach (var option in rootContextOptions.Options)
        {
            SetForwardedArguments(option);
        }
        ContextOptions = rootContextOptions;

        var queryOptions = new GetOptionsTreeQuery();
        OptionDto options = await _mediator.Send(queryOptions);

        Options = options;

        return View("SideBarViewComponent", this);
    }

    private void SetForwardedArguments(OptionDto option)
    {
        option.Url = _argumentFowardingService.SetForwardedArguments(option.Url);
        foreach (var childOption in option.Options)
        {
            SetForwardedArguments(childOption);
        }
    }
}

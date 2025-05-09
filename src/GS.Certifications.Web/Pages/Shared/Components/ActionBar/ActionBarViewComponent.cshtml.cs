using GSF.Application.Security.Options.GetOptions.Queries;
using GSF.Application.Security.Options.Queries;
using GSFSharedResources;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Common.Services;
using System.Threading.Tasks;

public class ActionBarViewComponent : ViewComponent
{
    public IStringLocalizer<Shared> loc { get; }
    public IHttpContextAccessor _httpContextAccessor { get; }

    private readonly IArgumentFowardingService _argumentFowardingService;

    public OptionDto Options { get; set; }
    public string CurrentPage { get; set; }

    public ActionBarViewComponent(IStringLocalizer<Shared> sharedLocalizer, IHttpContextAccessor httpContextAccessor, IArgumentFowardingService argumentFowardingService)
    {
        loc = sharedLocalizer;
        _httpContextAccessor = httpContextAccessor;
        _argumentFowardingService = argumentFowardingService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {

        IMediator _mediator = (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        CurrentPage = _httpContextAccessor.HttpContext.Items["navigationKey"].ToString();
        var queryPage = new GetOptionsTreeByPageQuery() { Page = CurrentPage };
        OptionDto optionsByPage = await _mediator.Send(queryPage);

        foreach (OptionDto option in optionsByPage.Options)
        {
            option.Url = _argumentFowardingService.SetForwardedArguments(option.Url);
        }

        Options = optionsByPage;
        return View("ActionBarViewComponent", this);
    }




}

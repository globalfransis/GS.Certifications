using GSF.Application.Security.Options.GetOptions.Queries;
using GSF.Application.Security.Options.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Socios.Web.Common.Helpers.Localizations;
using Socios.Web.Common.Resources;
using Socios.Web.Common.Services;

public class SideBarViewComponent : ViewComponent
{
    public IHttpContextAccessor _httpContextAccessor { get; }

    private readonly IArgumentFowardingService _argumentFowardingService;
    private LocalizationHelper<Shared> _localizationHelper;

    public OptionDto Options { get; set; }

    public OptionDto ContextOptions { get; set; }

    public bool HasContextMenu => ContextOptions.Options?.Any() ?? false;

    public string CurrentPage { get; set; }

    public SideBarViewComponent(IHttpContextAccessor httpContextAccessor, IArgumentFowardingService argumentFowardingService)
    {
        _httpContextAccessor = httpContextAccessor;
        _argumentFowardingService = argumentFowardingService;
        _localizationHelper = null;
    }

    public LocalizationHelper<Shared> LocalizationHelper { get
        {
            if(_localizationHelper is null)
            {
                var stringLocalizaer = (IStringLocalizer<Shared>)HttpContext.RequestServices.GetService(typeof(IStringLocalizer<Shared>));
                _localizationHelper = new LocalizationHelper<Shared>(stringLocalizaer);
            }
            return _localizationHelper;
        }
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

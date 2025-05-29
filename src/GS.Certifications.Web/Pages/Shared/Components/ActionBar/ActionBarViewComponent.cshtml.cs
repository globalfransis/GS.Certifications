using GSF.Application.Security.Options.GetOptions.Queries;
using GSF.Application.Security.Options.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Common.Services;
using System.Threading.Tasks;
using GS.Certifications.Web.Common.Resources;
using GS.Certifications.Web.Common.Helpers;

public class ActionBarViewComponent : ViewComponent
{
    public IHttpContextAccessor _httpContextAccessor { get; }

    private readonly IArgumentFowardingService _argumentFowardingService;

    private LocalizationHelper<Shared> _localizationHelper;

    public OptionDto Options { get; set; }
    public string CurrentPage { get; set; }

    public ActionBarViewComponent(IHttpContextAccessor httpContextAccessor, IArgumentFowardingService argumentFowardingService)
    {
        _localizationHelper = null; ;
        _httpContextAccessor = httpContextAccessor;
        _argumentFowardingService = argumentFowardingService;
    }

    public LocalizationHelper<Shared> LocalizationHelper
    {
        get
        {
            if (_localizationHelper is null)
            {
                var stringLocalizaer = (IStringLocalizer<Shared>)HttpContext.RequestServices.GetService(typeof(IStringLocalizer<Shared>));
                _localizationHelper = new LocalizationHelper<Shared>(stringLocalizaer);
            }
            return _localizationHelper;
        }
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

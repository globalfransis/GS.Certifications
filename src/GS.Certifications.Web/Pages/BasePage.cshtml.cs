using AutoMapper;
using GSF.Application.Security.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Web;

namespace GS.Certifications.Web.Pages;

[Authorize]
[ResponseCache(Location = ResponseCacheLocation.None, Duration = -1, NoStore = true)]
public class BasePageModel : PageModel
{
    private IMediator _mediator;
    private IStringLocalizer<GS.Certifications.Web.Common.Resources.Shared> _loc;
    private IMapper _mapper;
    private IConfiguration _configuration;

    private string _unspecifiedText;
    private string _allOptionsText;
    private string _selectOptionsText;
    private string _concurrencyExceptionText;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    protected IConfiguration Configuration => _configuration ??= HttpContext.RequestServices.GetService<IConfiguration>();
    protected IStringLocalizer<GS.Certifications.Web.Common.Resources.Shared> Loc => _loc ??= HttpContext.RequestServices.GetService<IStringLocalizer<GS.Certifications.Web.Common.Resources.Shared>>();
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
    protected string UnspecifiedText => _unspecifiedText ??= Loc[HttpUtility.HtmlDecode(Configuration.GetSection("Application").GetValue(typeof(string), "UnspecifiedOptionsText").ToString())];
    protected string AllOptionsText => _allOptionsText ??= Loc[HttpUtility.HtmlDecode(Configuration.GetSection("Application").GetValue(typeof(string), "AllOptionsText").ToString())];
    protected string SelectOptionsText => _selectOptionsText ??= Loc[HttpUtility.HtmlDecode(Configuration.GetSection("Application").GetValue(typeof(string), "SelectOptionsText").ToString())];
    protected string ConcurrencyExceptionText => _concurrencyExceptionText ??= Loc[HttpUtility.HtmlDecode(Configuration.GetSection("Application").GetValue(typeof(string), "ConcurrencyExceptionText").ToString())];


    protected List<SecurityGrantDto> grants;

    public bool ShowTitle { get; set; } = true;

    [ViewData]
    public string Title { get; set; }

    [TempData]
    public string PersistentMessage { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    [TempData]
    public string SuccessMessage { get; set; }

    [TempData]
    public string AlertMessage { get; set; }

    [TempData]
    public string PersistentErrorMessage { get; set; }

    protected IActionResult RedirectByModelState(string pageName, object routeValues)
    {
        if (ModelState.IsValid)
            return RedirectToPage(pageName, routeValues);
        else
            return Page();
    }

    protected IActionResult RedirectByModelState(string url)
    {
        if (ModelState.IsValid)
            return Redirect(url);
        else
            return Page();
    }

    public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
        base.OnPageHandlerSelected(context);
    }
}


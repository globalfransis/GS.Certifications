using GS.Certifications.Domain.Commons.Constants;
using GSF.Application.Common.Interfaces;
using GSF.Application.Interfaces;
using GSF.Application.Security.Users.Commands.PasswordChange;
using GSF.Application.Security.Users.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Serilog;
using Socios.Web.Pages;
using System.ComponentModel.DataAnnotations;

namespace Socios.Web.Areas.Security.Pages;

[AllowAnonymous]
public class LoginModel : BasePageModel
{
    private readonly ITargetPathResolver _targetPathResolver;
    private readonly IStringLocalizer<Login> _pageLoc;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IStandardLoginService _standardLoginService;

    //In this case is a Fixed value.
    private const long DomainFIdm = DomainFIdmConstants.Socios;

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Usuario_o_Email", ResourceType = typeof(Login))]
    public string Login_or_Email { get; set; }

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Contraseña", ResourceType = typeof(Login))]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [BindProperty, HiddenInput]
    public string ReturnUrl { get; set; }

    public bool PasswordRecoveryRequested { get; private set; } = false;

    public bool PasswordForgotten { get; set; } = false;

    public bool? SessionExpired { get; set; } = false;
    public string SessionExpiredMessage { get; set; }

    public IWebHostEnvironment WebHostEnvironment => _webHostEnvironment;

    public LoginModel(ITargetPathResolver targetPathResolver, IStandardLoginService standardLoginService,
                      IStringLocalizer<Login> pageLoc, IWebHostEnvironment webHostEnvironment)
    {

        _targetPathResolver = targetPathResolver;
        _pageLoc = pageLoc;
        _webHostEnvironment = webHostEnvironment;
        _standardLoginService = standardLoginService;
        Title = "Login";
    }

    public void OnGet(string returnUrl, bool? enablePasswordRecoveryMode)
    {
        SessionExpired = HttpContext.Request.Query?["SessionExpired"].Any();
        if (enablePasswordRecoveryMode == true) PasswordForgotten = true;

        if (SessionExpired.HasValue && SessionExpired.Value)
        {
            ReturnUrl = "/";
            _ = ModelState.TryAddModelError("SessionExpired", _pageLoc["Ha finalizado la sesión"]);
            SessionExpired = true;
            SessionExpiredMessage = _pageLoc["Ha finalizado la sesión. Vuelva a ingresar."];
        }

        if (Url.IsLocalUrl(returnUrl))
            ReturnUrl = returnUrl;
        else
            ReturnUrl = "/";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrEmpty(Login_or_Email))
        {
            var userLoginResult = await _standardLoginService.LogIn(Login_or_Email, Password, DomainFIdm);

            if (userLoginResult.IsAuthenticated)
            {
                return RedirectToPage("/Stepper");
            }
            else
            {
                //ModelState.AddModelError(nameof(Username), _stringLocalizer["Usuario o Password Incorrecto"]);
                ModelState.AddModelError(nameof(Login_or_Email), _pageLoc["Usuario o clave incorrecta."]);
                return await Task.FromResult(Page());
            }
        }
        else
        {
            ModelState.AddModelError(nameof(Login_or_Email), _pageLoc["Debe ingresar un usuario o email."]);
            return await Task.FromResult(Page());
        }
    }

    public async Task<IActionResult> OnPostPasswordRecoveryAsync()
    {
        PasswordForgotten = true;
        try
        {
            await Mediator.Send(new SendPasswordRecoveryTokenCommand() { Login_or_Email = Login_or_Email, DomainFIdm = DomainFIdmConstants.Socios });

            ViewData["PasswordChangeRequestedMessage"] = $"{_pageLoc["Se ha enviado un email a"]} <b>{Login_or_Email}</b>, {_pageLoc["por favor revise su casilla de correo."]}";
            PasswordRecoveryRequested = true;
        }
        catch (NonExistentUserException)
        {
            ModelState.AddModelError(nameof(Login_or_Email), _pageLoc["El usuario no existe. Complete con un email válido."]);
        }
        catch (SendNotificationException ex)
        {
            LocalizedString errorMessage = _pageLoc["Algo no salió bien al enviar la notificación de la recuperación de contraseña. Intentá más tarde o consultá a soporte."];
            Log.Logger.Error(ex, errorMessage);
            ModelState.AddModelError(string.Empty, errorMessage);
        }

        return Page();
    }

    public void OnPostPasswordForgotten()
    {
        PasswordForgotten = true;
    }

    public async Task<IActionResult> OnGetLogoutAsync()
    {
        await _standardLoginService.LogOut();

        return RedirectToPage("/Login", new { area = "Security" });
    }
}
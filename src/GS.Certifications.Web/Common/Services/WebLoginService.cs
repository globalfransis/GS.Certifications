using GSF.Application.Common.Interfaces;
using GSF.Application.Interfaces;
using GSF.Application.Security.Companies.Commands;
using GSF.Application.Security.Companies.Queries.GetCompaniesByUser;
using GSF.Application.Security.DomainFs.Commands;
using GSF.Application.Security.Grants.Queries;
using GSF.Application.Security.Sessions.Commands;
using GSF.Application.Security.Users.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using GS.Certifications.Web.Common;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Common.Services;

public class WebLoginService : IStandardLoginService
{
    private readonly HttpContext _httpContext;
    private readonly IMediator _mediator;
    private readonly ICurrentDomainService _currentDomainService;

    public WebLoginService(IHttpContextAccessor httpContextAccessor, IMediator mediator, ICurrentDomainService currentDomainService)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _mediator = mediator;
        _currentDomainService = currentDomainService;
    }

    public async Task<AuthenticatedUserDto> LogIn(string Login_or_Email, string Password, long DomainFIdm)
    {
        var command = new LoginUserCommand()
        {
            Login_or_Email = Login_or_Email,
            Password = Password,
            SessionId = _httpContext.Session.Id,
            DomainFIdm = DomainFIdm
        };

        AuthenticatedUserDto userLoginResult = await _mediator.Send(command);

        if (userLoginResult.IsAuthenticated)
        {
            // TODO: obtener de la base de datos
            const string CSS_THEME = ""; // "red_theme";
            const string LOGO_IMAGE = "ColoniaExpressLogo.png";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{userLoginResult.Name} {userLoginResult.LastName}"),
                new Claim(ClaimTypes.Email, userLoginResult.Email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, userLoginResult.Login),
                new Claim(Constants.ClaimSystemUser, userLoginResult.SystemUse.ToString()),
                new Claim("css_theme", CSS_THEME),
                new Claim("logo", LOGO_IMAGE),
                new Claim("user_id", userLoginResult.Id.ToString()),
                //new Claim("domaindF_id", userLoginResult.DomainFIdm.ToString()),
            };

            var BoardingIdentity = new ClaimsIdentity(claims, "BoardingIdentity");
            var userPrincipal = new ClaimsPrincipal(new[] { BoardingIdentity });

            await _httpContext.SignInAsync(userPrincipal);
            _httpContext.Session.SetString(Constants.SessionAliveFlagKey, string.Empty);

            //////////////////////////////////////////////////////////////////
            // Estas queries almacenan los datos en sesión.
            //////////////////////////////////////////////////////////////////

            //This should be the first thing to do, as other command will use the value setted in this command.
            var setCurrentDomainF = new SetCurrentDomainFCommand() { DomainFIdm = DomainFIdm };
            await _mediator.Send(setCurrentDomainF);

            var queryCompanies = new GetCompaniesByUserQuery() { UserId = userLoginResult.Id };
            var companies = await _mediator.Send(queryCompanies);

            long? companyId = companies.FirstOrDefault()?.Id;

            if (long.TryParse(_httpContext.Request.Cookies["boarding.companyId"], out long companyResultId) &&
               companies.Any(c => c.Id == companyResultId))
            {
                companyId = companyResultId;
            }

            var setCurrentCompany = new SetCurrentCompanyCommand() { CompanyId = companyId.Value, UserId = userLoginResult.Id };
            await _mediator.Send(setCurrentCompany);

            var getGrantsByUser = new LoadGrantsByUserQuery() { UserId = userLoginResult.Id };
            await _mediator.Send(getGrantsByUser);
        }

        return userLoginResult;
    }

    public async Task LogOut()
    {
        var sessionGuid = _httpContext.Session.Id;

        await _httpContext.SignOutAsync();

        _httpContext.Session.Clear();
        _httpContext.Response.Cookies.Delete("Boarding.Session");

        var closeSessionByLogoutCommand = new CloseSessionByLogoutCommand() { SessionGuid = sessionGuid };
        await _mediator.Send(closeSessionByLogoutCommand);
    }
}

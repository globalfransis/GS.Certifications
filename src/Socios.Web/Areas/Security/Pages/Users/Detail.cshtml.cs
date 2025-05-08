using AutoMapper;
using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Security.Users.Commands.DeleteUser;
using GSF.Application.Security.Users.Queries.GetUserCrud;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Socios.Web.Areas.Security.Pages.Users;

public class DetailUserModel : UserCrudModel
{
    #region ctor/private

    public DetailUserModel(IMapper mapper,
                           ICurrentCompanyService currentCompanyService,
                           ICurrentUserService currentUserService,
                           IStringLocalizer<Shared> loc,
                           IStringLocalizer<SecurityResources> secLoc) : base(mapper, currentCompanyService, currentUserService, loc)
    {
        Title = secLoc["Detalle de Usuario"];
        Mode = "Detalle";
    }
    #endregion

    public async Task<IActionResult> OnGet(long id)
    {
        LoggedUserSystemUse = LoggedUserSystemUse.HasValue ? LoggedUserSystemUse.Value : _currentUserService.SystemUse;
        LoggedUserId = LoggedUserId == default ? _currentUserService.UserId : LoggedUserId;
        IActionResult result = null;
        UserCrudDto usuarioDto = await LoadUser(id);
        if (usuarioDto == null)
        {
            ErrorMessage = _loc["El usuario ya no existe"];
            result = RedirectByModelState("/Users/Index", new { area = "Security" });
        }
        return result ?? Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, byte[] rowVersion)
    {
        IActionResult result;
        DeleteUserCommand command = new DeleteUserCommand() { Id = id, RowVersion = rowVersion };
        try
        {
            await OnGet(id);
            await Mediator.Send(command);
            result = RedirectByModelState("/Users/Index", new { area = "Security" });
        }
        catch (DbUpdateConcurrencyException)
        {
            ErrorMessage = _loc["Los datos fueron modificados por otro usuario. Intente nuevamente."];
            result = RedirectToPage("/Users/Index", new { area = "Security" });
        }

        return result;
    }
}

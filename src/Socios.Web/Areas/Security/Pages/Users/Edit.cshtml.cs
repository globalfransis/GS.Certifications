using AutoMapper;
using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Security.Users.Commands.UpdateUser;
using GSF.Application.Security.Users.Queries.GetUserCrud;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Socios.Web.Hubs;

namespace Socios.Web.Areas.Security.Pages.Users;

public class EditUserModel : UserCrudModel
{
    private IHubContext<NotificationHub> _notificationHubContext;

    #region ctor/private
    public EditUserModel(IMapper mapper, ICurrentUserService currentUserService,
                         ICurrentCompanyService currentCompanyService,
                         IStringLocalizer<Shared> loc,
                         IStringLocalizer<SecurityResources> secLoc,
                         IHubContext<NotificationHub> notificationHubContext) : base(mapper, currentCompanyService, currentUserService, loc)
    {
        Title = secLoc["Modificar Usuario"];
        Mode = "Modificación";
        _notificationHubContext = notificationHubContext;
    }
    #endregion

    public async Task<IActionResult> OnGet(long id)
    {
        IActionResult result = null;
        UserCrudDto usuarioDto = await LoadUser(id);
        if (usuarioDto == null)
        {
            ErrorMessage = _loc["El usuario ya no existe"];
            result = RedirectByModelState("/Users/Detail", new { area = "Security", id = Id });
        }
        else
        {
            LoggedUserId = _currentUserService.UserId;
            LoggedUserSystemUse = LoggedUserSystemUse.HasValue ? LoggedUserSystemUse.Value : _currentUserService.SystemUse;
            CurrentGroupId = CurrentGroupId == default ? (await _currentCompanyService.GetCurrentCompanyGroupAsync()).Id : CurrentGroupId;
            CurrentCompanyId = CurrentCompanyId == default ? (await _currentCompanyService.GetCurrentCompanyAsync()).Id : CurrentCompanyId;
            await LoadOrganizations();
            if (OrganizationsSelectList != null || OrganizationsSelectList.Count() > 0)
            {
                OrganizationId = int.Parse(OrganizationsSelectList.FirstOrDefault().Value);
                await LoadCompanies();
                await LoadGroups();
                await SetRelationshipStatus();
                RemoveCompaniesInUse();
            }
        }
        return result ?? Page();
    }

    public async Task<IActionResult> OnPostSave()
    {
        CurrentGroupId = CurrentGroupId == default ? (await _currentCompanyService.GetCurrentCompanyGroupAsync()).Id : CurrentGroupId;

        var command = new UpdateUserCommand()
        {
            Id = Id,
            Login = Login,
            FirstName = FirstName,
            LastName = LastName,
            IdNumber = IdNumber,
            PhoneNumber = PhoneNumber,
            Email = Email,
            CompaniesUsersGroups = from cug in CompaniesUsersGroups
                                   select (cug.CompanyId, cug.GroupId),
            GroupOwnerId = CurrentGroupId,
            RowVersion = RowVersion,
            SystemUse = SystemUse
        };

        try
        {
            await LoadControls();
            await SetRelationshipStatus();
            RemoveCompaniesInUse();
            await Mediator.Send(command);
            SuccessMessage = _loc["Se ha modificado el usuario {0}.", FirstName];
            await _notificationHubContext.Clients.User(command.Login).SendAsync("ReceiveNotification", "Se ha modificado su perfil.");
            return RedirectByModelState("/Users/Detail", new { area = "Security", id = Id });
        }
        catch (DbUpdateConcurrencyException)
        {
            ErrorMessage = _loc["Los datos fueron modificados por otro usuario. Intente nuevamente."];
            return RedirectToPage("/Users/Index", new { area = "Security" });
        }
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        await LoadControls();

        UserCrudCompanyUserGroupDto relationship = new UserCrudCompanyUserGroupDto()
        {
            CompanyId = CompanyId,
            UserId = Id,
            GroupId = GroupId,
            Modifiable = true
        };

        relationship.GroupName = GroupsSelectList.FirstOrDefault(g => g.Value == GroupId.ToString()).Text;
        relationship.CompanyName = CompaniesSelectList.FirstOrDefault(g => g.Value == CompanyId.ToString()).Text;

        CompaniesUsersGroups.Add(relationship);
        RemoveCompaniesInUse();
        ModelState.Clear();

        return await Task.FromResult(Page());
    }
}

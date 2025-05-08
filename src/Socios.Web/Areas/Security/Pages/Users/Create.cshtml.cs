using AutoMapper;
using GSF.Application.Common.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Security.Users.Commands.CreateUser;
using GSF.Application.Security.Users.Queries.GetUserCrud;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Socios.Web.Areas.Security.Pages.Users;

public class CreateUserModel : UserCrudModel
{

    #region ctor/private
    public CreateUserModel(IMapper mapper, ICurrentUserService currentUserService,
                           ICurrentCompanyService currentCompanyService,
                           IStringLocalizer<Shared> loc,
                           IStringLocalizer<SecurityResources> secLoc) : base(mapper, currentCompanyService, currentUserService, loc)
    {
        Title = secLoc["Crear Usuario"];
        Mode = "Alta";
    }
    #endregion

    public async Task OnGet()
    {
        Password = Password;
        LoggedUserId = _currentUserService.UserId;
        LoggedUserSystemUse = LoggedUserSystemUse.HasValue ? LoggedUserSystemUse.Value : _currentUserService.SystemUse;
        await LoadOrganizations();
        if (OrganizationsSelectList != null && OrganizationsSelectList.Count() > 0)
        {
            OrganizationId = int.Parse(OrganizationsSelectList.FirstOrDefault().Value);
            await LoadCompanies();
            await LoadGroups();
        }
    }

    public async Task<IActionResult> OnPostSave()
    {
        long currentGroupId = (await _currentCompanyService.GetCurrentCompanyGroupAsync()).Id;

        var command = new CreateUserCommand()
        {
            Login = Login,
            FirstName = FirstName,
            LastName = LastName,
            IdNumber = IdNumber,
            Password = Password,
            PhoneNumber = PhoneNumber,
            Email = Email,
            CompaniesUsersGroups = from cug in CompaniesUsersGroups
                                   select (cug.CompanyId, cug.GroupId),
            GroupOwnerId = currentGroupId,
            SystemUse = SystemUse
        };

        await LoadControls();
        RemoveCompaniesInUse();

        Id = await Mediator.Send(command);
        SuccessMessage = _loc["Se ha creado el usuario {0}", FirstName];
        return RedirectByModelState("/Users/Detail", new { area = "Security", id = Id });
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        await LoadControls();

        UserCrudCompanyUserGroupDto relationship = new UserCrudCompanyUserGroupDto()
        {
            CompanyId = CompanyId,
            GroupId = GroupId,
            Modifiable = true
        };

        relationship.GroupName = GroupsSelectList.FirstOrDefault(g => g.Value == GroupId.ToString()).Text;
        relationship.CompanyName = CompaniesSelectList.FirstOrDefault(g => g.Value == CompanyId.ToString()).Text;

        CompaniesUsersGroups.Add(relationship);
        RemoveCompaniesInUse();
        ModelState.Clear();
        Password = Password;
        return await Task.FromResult(Page());
    }
}

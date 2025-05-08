using AutoMapper;
using GSF.Application.Security.Roles.Commands.UpdateRole;
using GSF.Application.Security.Services.CurrentCompany;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Socios.Web.Areas.Security.Pages.Roles;

public class EditRoleModel : RoleCrudModel
{
    private readonly ICurrentCompanyService _currentCompanyService;

    #region ctor/private

    public EditRoleModel(IMapper mapper,
                         ICurrentCompanyService currentCompanyService,
                         IStringLocalizer<Shared> loc) : base(mapper, loc)
    {
        Title = loc["Modificar Rol"];
        Mode = "Modificación";
        _currentCompanyService = currentCompanyService;
    }
    #endregion

    public async Task<IActionResult> OnGet(int id)
    {
        await GenerateOptionTree();

        return await LoadRole(id);
    }

    public async Task<IActionResult> OnPost()
    {
        IActionResult result;

        if (!string.IsNullOrEmpty(SelectedValues))
            GenerateListsForSave();

        CurrentGroupId = CurrentGroupId == default ? _currentCompanyService.GetCurrentCompanyGroupAsync().GetAwaiter().GetResult().Id : CurrentGroupId;

        var command = new UpdateRoleCommand()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            GroupOwnerId = CurrentGroupId,
            SelectedGrants = SelectedGrants.ToList(),
            SelectedOptions = SelectedOptions.ToList(),
            RowVersion = RowVersion
        };

        try
        {
            await Mediator.Send(command);
            SuccessMessage = _loc["Se ha modificado el Rol {0}", Name];
            result = RedirectByModelState("/Roles/Detail", new { area = "Security", id = Id });

        }
        catch (DbUpdateConcurrencyException)
        {
            ErrorMessage = _loc["Los datos fueron modificados por otro usuario. Intente nuevamente."];

            result = RedirectToPage("/Roles/Index", new { area = "Security" });
        }
        return result;
    }
}

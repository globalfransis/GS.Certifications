using AutoMapper;
using GSF.Application.Security.Groups.Commands.DeleteGroup;
using GSF.Application.Security.Groups.Queries.GetGroupCrud;
using GSF.Application.Security.Services.CurrentCompany;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Socios.Web.Areas.Security.Pages.Groups;

public class DetailGroupModel : GroupCrudModel
{
    #region ctor/private

    public DetailGroupModel(IMapper mapper,
                            ICurrentCompanyService currentCompanyService,
                            IStringLocalizer<Shared> loc) : base(mapper, currentCompanyService, loc)
    {
        Title = loc["Detalle de Grupo"];
        Mode = "Detalle";
    }
    #endregion

    public async Task<IActionResult> OnGet(int id)
    {
        IActionResult result = null;
        GroupCrudDto groupDto = await LoadGroup(id);
        if (groupDto == null)
        {
            ErrorMessage = _loc["El Grupo ya no existe"];
            result = RedirectByModelState("/Groups/Index", new { area = "Security" });
        }
        OrganizationsSelectList = new List<SelectListItem>();
        RoleSelectList = new List<SelectListItem>();
        return result ?? Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, byte[] rowVersion)
    {
        IActionResult result;
        DeleteGroupCommand command = new DeleteGroupCommand() { Id = id, RowVersion = rowVersion };
        _ = await OnGet(id);
        try
        {
            await Mediator.Send(command);
            result = RedirectByModelState("/Groups/Index", new { area = "Security" });
        }
        catch (DbUpdateConcurrencyException)
        {
            ErrorMessage = _loc["Los datos fueron modificados por otro usuario. Intente nuevamente."];
            result = RedirectToPage("/Groups/Index", new { area = "Security" });
        }

        return result;
    }
}

using AutoMapper;
using GSF.Application.Security.Roles.Commands.DeleteRole;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Areas.Security.Pages.Roles;

public class DetailRoleModel : RoleCrudModel
{
    public DetailRoleModel(IMapper mapper,
                           IStringLocalizer<Shared> loc) : base(mapper, loc)
    {
        Title = loc["Detalle de Rol"];
        Mode = "Detalle";
    }
    public async Task<IActionResult> OnGetAsync(int id)
    {
        await GenerateOptionTree();

        return await LoadRole(id);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, byte[] rowVersion)
    {
        IActionResult result;
        DeleteRoleCommand command = new DeleteRoleCommand() { Id = id, RowVersion = rowVersion };
        try
        {
            await Mediator.Send(command);
            result = RedirectByModelState("/Roles/Index", new { area = "Security" });
        }
        catch (DbUpdateConcurrencyException)
        {
            ErrorMessage = _loc["Los datos fueron modificados por otro usuario. Intente nuevamente."];
            result = RedirectToPage("/Roles/Index", new { area = "Security" });
        }

        return result;
    }
}
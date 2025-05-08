using AutoMapper;
using GSF.Application.Configuration.Notifications.Commands.DeleteNotificationTypeOrganization;
using GSF.Application.Configuration.Notifications.Queries;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Socios.Web.Areas.Configuration.Pages.NotificationTypes;

public class DetailNotificationTypeOrganizationModel : NotificationTypeCrudModel
{

    #region ctor/private

    public DetailNotificationTypeOrganizationModel(IMapper mapper, IStringLocalizer<Shared> loc) : base(mapper, loc)
    {
        Title = loc["Detalle de Tipo de Notificación"];
        Mode = "Detalle";
    }
    #endregion

    public async Task<IActionResult> OnGet(int id)
    {
        IActionResult result = null;
        NotificationTypesOrganizationsDto notificationTypeOrgDto = await LoadNotificationTypeOrganization(id);
        if (notificationTypeOrgDto == null)
        {
            ErrorMessage = _loc["La relación entre el Tipo de Notificación y la Organización ya no existe"];
            result = RedirectByModelState("/NotificationTypes/Index", new { area = "Configuration" });
        }
        NotificationTypesList = new List<NotificationTypesDto>();
        NotificationTypesList.Add(new NotificationTypesDto()
        {
            Id = notificationTypeOrgDto.NotificationTypeId,
            Name = notificationTypeOrgDto.NotificationType.Name,
            Description = notificationTypeOrgDto.NotificationType.Description,
            NotificationLevel = notificationTypeOrgDto.NotificationType.NotificationLevel
        });
        NotificationTypeId = notificationTypeOrgDto.NotificationTypeId;
        GenerateJsonForPage();
        NotificationTypeSelectList = new List<SelectListItem>();
        GroupsSelectList = new List<SelectListItem>();
        return result ?? Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id, byte[] rowVersion)
    {
        IActionResult result;
        DeleteNotificationTypeOrganizationCommand command = new DeleteNotificationTypeOrganizationCommand() { Id = id, RowVersion = rowVersion };
        try
        {
            await Mediator.Send(command);
            SuccessMessage = _loc["Se ha eliminado la relación entre el Tipo de Notificación y la Organización"];
            result = RedirectByModelState("/NotificationTypes/Index", new { area = "Configuration" });
        }
        catch (DbUpdateConcurrencyException)
        {
            ErrorMessage = _loc["Los datos fueron modificados por otro usuario. Intente nuevamente."];
            result = RedirectToPage("/NotificationTypes/Index", new { area = "Configuration" });
        }
        result = RedirectByModelState("/NotificationTypes/Index", new { area = "Configuration" });

        return result;
    }
}

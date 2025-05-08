using AutoMapper;
using GSF.Application.Configuration.Notifications.Commands.UpdateNotificationTypeOrganization;
using GSF.Application.Configuration.Notifications.Queries;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Socios.Web.Areas.Configuration.Pages.NotificationTypes;

public class EditNotificationTypeOrganizationModel : NotificationTypeCrudModel
{

    #region ctor/private

    public EditNotificationTypeOrganizationModel(IMapper mapper, IStringLocalizer<Shared> loc) : base(mapper, loc)
    {
        Title = loc["Modificar Tipo de Notificación"];
        Mode = "Modificación";
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
        else
        {
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
            await LoadGroups();
            UpdateSelectLists();
        }
        return result ?? Page();
    }

    public async Task<IActionResult> OnPostSave()
    {
        IActionResult result;
        var command = new UpdateNotificationTypeOrganizationCommand()
        {
            Id = Id,
            SelectedGroups = from r in SelectedGroups
                             select r.Id,
            RowVersion = RowVersion,
        };

        try
        {
            await LoadGroups();
            GenerateNotificationTypeListFromPage();
            UpdateSelectLists();

            await Mediator.Send(command);
            SuccessMessage = _loc["Se ha editado la relación entre la Organización y la Notificación Tipo: {0}", NotificationTypesList.FirstOrDefault(ntl => ntl.Id == NotificationTypeId).Name];
            result = RedirectByModelState("/NotificationTypes/Detail", new { area = "Configuration", id = Id });
        }
        catch (DbUpdateConcurrencyException)
        {
            ErrorMessage = _loc["Los datos fueron modificados por otro usuario. Intente nuevamente."];
            result = RedirectToPage("/NotificationTypes/Index", new { area = "Configuration" });
        }

        return result;
    }
}

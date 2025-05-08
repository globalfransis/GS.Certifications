using AutoMapper;
using GSF.Application.Configuration.Notifications.Commands.CreateNotificationTypeOrganization;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Socios.Web.Areas.Configuration.Pages.NotificationTypes;

public class CreateNotificationTypeOrganizationModel : NotificationTypeCrudModel
{

    #region ctor/private

    public CreateNotificationTypeOrganizationModel(IMapper mapper, IStringLocalizer<Shared> loc) : base(mapper, loc)
    {
        Title = loc["Relacionar Tipo de Notificación"];
        Mode = "Alta";
    }
    #endregion

    public async Task OnGet()
    {
        await LoadControls();

        var defaultNotificationtype = NotificationTypeSelectList.FirstOrDefault();
        if (defaultNotificationtype != null)
            NotificationTypeId = int.Parse(NotificationTypeSelectList.FirstOrDefault().Value);
    }

    public async Task<IActionResult> OnPostSave()
    {
        var command = new CreateNotificationTypeOrganizationCommand()
        {
            NotificationTypeId = NotificationTypeId,
            SelectedGroups = from r in SelectedGroups
                             select r.Id,
        };

        await LoadControls();
        UpdateSelectLists();

        Id = await Mediator.Send(command);
        SuccessMessage = _loc["Se ha creado la relación entre la Organización y la Notificación Tipo: {0}", NotificationTypesList.FirstOrDefault(ntl => ntl.Id == NotificationTypeId).Name];
        return RedirectByModelState("/NotificationTypes/Detail", new { area = "Configuration", id = Id });
    }
}

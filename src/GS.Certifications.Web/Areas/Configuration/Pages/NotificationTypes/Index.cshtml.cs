using GSF.Application.Configuration.Notifications.Queries;
using GSF.Application.Configuration.Notifications.Queries.GetNotificationTypes;
using GSF.Application.Configuration.Notifications.Queries.GetNotificationTypesOrganizations;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GS.Certifications.Web.Areas.Configuration.Pages.NotificationTypes;


public class IndexNotificationTypesModel : BasePageModel
{
    private readonly IStringLocalizer<Shared> _loc;
    //[Display(Name = "Alcance")]
    [BindProperty] public List<SelectListItem> NotificationLevelSelectList { get; private set; } = new List<SelectListItem>();

    [Display(Name = "Alcance de Notificación")]
    [BindProperty(SupportsGet = true)] public long NotificationLevelId { get; set; }

    [BindProperty] public List<SelectListItem> NotificationTypeSelectList { get; private set; } = new List<SelectListItem>();

    [Display(Name = "Tipo de Notificación")]
    [BindProperty(SupportsGet = true)] public long NotificationTypeId { get; set; }

    [BindProperty] public List<NotificationTypesOrganizationsDto> ActiveNotifications { get; private set; } = new List<NotificationTypesOrganizationsDto>();

    private readonly IConfiguration _config;

    public IndexNotificationTypesModel(IStringLocalizer<Shared> loc, IConfiguration configuration)
    {
        Title = loc["Notificaciones por Organización"];
        _loc = loc;
        _config = configuration;
    }

    public async Task OnGetAsync()
    {
        await LoadControls();
        await LoadNotificationTypesByOrganization();
    }

    private async Task LoadNotificationTypesByOrganization()
    {
        var query = new GetNotificationTypesOrganizationsQuery()
        {
            NotificationTypeId = NotificationTypeId,
        };
        if (NotificationLevelId != default)
            query.NotificationTypeLevel = NotificationLevelSelectList.FirstOrDefault(f => f.Value == NotificationLevelId.ToString()).Text;

        ActiveNotifications = await Mediator.Send(query);
    }

    private async Task LoadControls()
    {
        await LoadNotificationTypes();
        LoadNotificationLevels();
    }

    private void LoadNotificationLevels()
    {
        NotificationLevelSelectList.Add(new SelectListItem()
        {
            Value = "0",
            Text = _loc[HttpUtility.HtmlDecode(_config.GetSection("Application").GetValue(typeof(string), "UnspecifiedOptionsText").ToString())]
        });

        NotificationLevelSelectList.Add(new SelectListItem()
        {
            Value = "1",
            Text = "Empresa",
        });

        NotificationLevelSelectList.Add(new SelectListItem()
        {
            Value = "2",
            Text = "Organización",
        });

        if (NotificationLevelId != default)
        {
            SelectListItem selectedType = NotificationLevelSelectList.FirstOrDefault(item => item.Value == NotificationLevelId.ToString());
            selectedType.Selected = true;
        }
        //else
        //{
        //    SelectListItem selectedType = NotificationTypeSelectList.FirstOrDefault(item => item.Value == NotificationTypeId.ToString());
        //    selectedType.Selected = true;
        //}
    }

    private async Task LoadNotificationTypes()
    {
        var query = new GetNotificationTypesQuery();
        List<NotificationTypesDto> NotificationTypesList = await Mediator.Send(query);

        NotificationTypeSelectList.Add(new SelectListItem
        {
            Value = "0",
            Text = _loc[HttpUtility.HtmlDecode(_config.GetSection("Application").GetValue(typeof(string), "UnspecifiedOptionsText").ToString())]
        });
        NotificationTypeSelectList.AddRange(NotificationTypesList.Select(nt => new SelectListItem
        {
            Value = nt.Id.ToString(),
            Text = nt.Name,
        }).ToList());

        if (NotificationTypeId != default)
        {
            SelectListItem selectedType = NotificationTypeSelectList.FirstOrDefault(item => item.Value == NotificationTypeId.ToString());
            selectedType.Selected = true;
        }
        //else
        //{
        //    SelectListItem selectedType = NotificationTypeSelectList.FirstOrDefault(item => item.Value == NotificationTypeId.ToString());
        //    selectedType.Selected = true;
        //}

    }
}
using AutoMapper;
using GSF.Application.Configuration.Notifications.Queries;
using GSF.Application.Configuration.Notifications.Queries.GetNotificationTypes;
using GSF.Application.Configuration.Notifications.Queries.GetNotificationTypesOrganizations;
using GSF.Application.Configuration.Notifications.Queries.GetNotificationTypesOrganizationsGroups;
using GSF.Application.Segurity.Companies.Queries.GetGroupsByCurrentGroupOwner;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using GS.Certifications.Web.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static GSF.Application.Configuration.Notifications.Queries.NotificationTypesOrganizationsDto;

namespace GS.Certifications.Web.Areas.Configuration.Pages.NotificationTypes;


public class NotificationTypeCrudModel : BasePageModel
{
    [BindProperty] public string Mode { get; set; } = "";

    [BindProperty] public long Id { get; set; }

    [Display(Name = "Nombre de Usuario")]
    [BindProperty] public string NotificationTypeName { get; set; }

    [BindProperty] public DateTime Modified { get; set; }

    public List<SelectListItem> NotificationTypeSelectList { get; set; } = new List<SelectListItem>();

    [Display(Name = "Tipo de Notificación")]
    [BindProperty(SupportsGet = true)] public long NotificationTypeId { get; set; }
    [Display(Name = "Grupo")]
    [BindProperty] public long SelectedGroupId { get; set; }
    public IEnumerable<SelectListItem> GroupsSelectList { get; set; } = new List<SelectListItem>();

    [BindProperty] public string NotificationTypesJson { get; set; }

    public List<NotificationTypesDto> NotificationTypesList { get; set; }
    public List<GroupListDto> GroupList { get; set; }
    [Display(Name = "Grupo")]
    [BindProperty] public List<GroupSimpleModel> SelectedGroups { get; set; } = new List<GroupSimpleModel>();
    [HiddenInput][BindProperty] public byte[] RowVersion { get; set; } = new byte[] { 0 }; //Must be initializated, if not the binding in the create page (using a partialView) will have a modelState invalid.


    protected readonly IMapper _mapper;
    protected readonly IStringLocalizer<Shared> _loc;

    public NotificationTypeCrudModel(IMapper mapper,
                                     IStringLocalizer<Shared> loc)
    {
        _mapper = mapper;
        _loc = loc;
    }

    protected async Task LoadControls()
    {
        var activeNotificationTypes = await LoadNotificationTypesByOrganization();
        await LoadNotificationTypes(activeNotificationTypes);
        await LoadGroups();

        GenerateJsonForPage();
    }

    protected void GenerateJsonForPage()
    {
        NotificationTypesJson = JsonConvert.SerializeObject(NotificationTypesList);
    }

    private async Task LoadNotificationTypes(List<NotificationTypesOrganizationsDto> activeNotificationTypes)
    {
        var query = new GetNotificationTypesQuery();
        NotificationTypesList = await Mediator.Send(query);

        //NotificationTypeSelectList.Add(new SelectListItem
        //{
        //    Value = "0",
        //    Text = _loc[" - "]
        //});
        NotificationTypeSelectList.AddRange(NotificationTypesList
            .Where(ntDto => !activeNotificationTypes.Where(ant => ant.NotificationTypeId == ntDto.Id).Any())
            .Select(ntDto => new SelectListItem
            {
                Value = ntDto.Id.ToString(),
                Text = ntDto.Name,
            }).ToList());

        if (NotificationTypeId != default)
        {
            SelectListItem selectedType = NotificationTypeSelectList.FirstOrDefault(item => item.Value == NotificationTypeId.ToString());
            if (selectedType != null)
                selectedType.Selected = true;
        }
    }

    protected async Task LoadGroups()
    {
        var query = new GetGroupsByCurrentGroupOwnerQuery();
        GroupList = await Mediator.Send(query);
        GroupsSelectList = GroupList.Distinct().Select(a => new SelectListItem
        {
            Value = a.Id.ToString(),
            Text = a.Name
        }).ToList();
    }

    private async Task<List<NotificationTypesOrganizationsDto>> LoadNotificationTypesByOrganization()
    {
        var query = new GetNotificationTypesOrganizationsQuery();

        return await Mediator.Send(query);
    }

    protected void GenerateNotificationTypeListFromPage()
    {
        var deserializedNotificationTypes = JsonConvert.DeserializeObject<List<NotificationTypesDto>>(NotificationTypesJson);

        NotificationTypesDto dto = deserializedNotificationTypes.FirstOrDefault();
        NotificationTypesList = new List<NotificationTypesDto>();
        NotificationTypesList.Add(new NotificationTypesDto()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            NotificationLevel = dto.NotificationLevel
        });
        NotificationTypeId = dto.Id;
    }

    public async Task<IActionResult> OnPostAddGroupAsync()
    {
        if (Mode == "Alta")
            await LoadControls();
        else
        {
            await LoadGroups();
            GenerateNotificationTypeListFromPage();
        }

        GroupListDto selectedRole = GroupList.FirstOrDefault(r => r.Id == SelectedGroupId);

        if (!SelectedGroups.Where(g => g.Id == SelectedGroupId).Any() && selectedRole != null)
        {
            GroupSimpleModel relationship = new GroupSimpleModel()
            {
                Id = selectedRole.Id,
                Name = selectedRole.Name,
                Description = selectedRole.Description
            };

            SelectedGroups.Add(relationship);
        }
        else
            ErrorMessage = _loc["Ya existe una relación con el Rol seleccionado."];

        UpdateSelectLists();
        return await Task.FromResult(Page());
    }

    /// <summary>
    /// Get the combo list cleaned.
    /// </summary>
    /// <returns></returns>
    protected void UpdateSelectLists()
    {
        GroupsSelectList = GroupsSelectList.Where(sl => !SelectedGroups.Any(g => g.Id.ToString() == sl.Value)).ToList();
    }

    public async Task<IActionResult> OnPostRemoveGroupAsync(int groupIndex)
    {
        SelectedGroups.RemoveAt(groupIndex);
        if (Mode == "Alta")
            await LoadControls();
        else
        {
            await LoadGroups();
            GenerateNotificationTypeListFromPage();
        }
        UpdateSelectLists();
        ModelState.Clear();
        return await Task.FromResult(Page());
    }

    protected async Task<NotificationTypesOrganizationsDto> LoadNotificationTypeOrganization(long id)
    {
        var query = new GetNotificationTypesOrganizationsGroupsQuery() { NotificacionTypeOrganizationId = id };
        NotificationTypesOrganizationsDto notificationtypeOrgDto = await Mediator.Send(query);

        //Ver si cambiamos para que la validacion en la query y devuelva una excepcion.
        if (notificationtypeOrgDto == null)
            ErrorMessage = _loc["La relación entre el Tipo de Notificación y la Organización ya no existe"];
        else
            _mapper.Map(notificationtypeOrgDto, this);
        return notificationtypeOrgDto;
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NotificationTypesOrganizationsDto, NotificationTypeCrudModel>()
                .ForMember(dst => dst.SelectedGroups, opt => opt.MapFrom(src => src.Groups))
                .ForMember(dst => dst.NotificationTypeName, opt => opt.MapFrom(src => src.NotificationType.Name));
        }
    }

    public class GroupSimpleModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<NotificationTypeOrganizationGroupDto, GroupSimpleModel>();
            }
        }
    }
}

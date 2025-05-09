using AutoMapper;
using GSF.Application.Common.Mappings;
using GSF.Application.Security.Groups.Queries.GetGroupsByCurrentGroupOwnerAndOrganization;
using GSF.Application.Security.Services;
using GSF.Application.Security.Services.CurrentCompany;
using GSF.Application.Security.Users.Queries.GetUserCrud;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Areas.Security.Pages.Users.Modals;

public class UserDelegationPartialModel : IMapFrom<UserCrudDto>
{
    [BindProperty] public long Id { get; set; }
    [Display(Name = "Nombre de Usuario")]
    [BindProperty] public string Login { get; set; }
    [Display(Name = "Nombre")]
    [BindProperty] public string FirstName { get; set; }
    [Display(Name = "Apellido")]
    [BindProperty] public string LastName { get; set; }

    [BindProperty] public string Modified { get; set; }
    [HiddenInput][BindProperty] public byte[] RowVersion { get; set; }
    [BindProperty] public bool SystemUse { get; set; }
    [BindProperty] public long CurrentGroupOwnerId { get; set; }
    [Display(Name = "Grupo Dueño Actual")]
    [BindProperty] public string CurrentGroupOwnerName { get; set; }
    [Display(Name = "Grupo Dueño")]
    [BindProperty] public long SelectedGroupOwnerId { get; set; }

    public IEnumerable<SelectListItem> GroupsSelectList { get; set; }

    private readonly ICurrentCompanyService _currentCompanyService;
    private readonly IMediator _mediator;

    public UserDelegationPartialModel(ICurrentCompanyService currentCompanyService,
                                      IMediator mediator)
    {
        _currentCompanyService = currentCompanyService;
        _mediator = mediator;
    }

    public async Task Initialize()
    {
        var queryGroups = new GetGroupsByCurrentGroupOwnerAndOrganizationQuery();
        List<SecurityGroupDto> groupDto = await _mediator.Send(queryGroups);
        //GroupsSelectList = groupDto.Select(a => new SelectListItem
        //{
        //    Value = a.Id.ToString(),
        //    Text = a.Name
        //}).ToList();

        //SelectedGroupOwnerId = CurrentGroupOwnerId;
        GroupsSelectList = groupDto.Where(gDto => gDto.Id != CurrentGroupOwnerId && (!SystemUse || SystemUse && gDto.SystemUse))
            .Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCrudDto, UserDelegationPartialModel>()
                .ForMember(dst => dst.Modified, opt => opt.MapFrom(src => src.Modified.Value.ToString()))
                .ForMember(dst => dst.CurrentGroupOwnerId, opt => opt.MapFrom(dst => dst.GroupOwnerId))
                .ForMember(dst => dst.CurrentGroupOwnerName, opt => opt.MapFrom(dst => dst.GroupOwnerName))
                .ForMember(dst => dst.Modified, opt => opt.MapFrom(dst => dst.Modified.HasValue ? dst.Modified.Value.ToString() : ""));
        }
    }
}

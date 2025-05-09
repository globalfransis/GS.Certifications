using AutoMapper;
using GSF.Application.Global.i18n.Cultures.Commands.UpdateCulture;
using GSF.Application.Global.i18n.Cultures.Queries;
using GSF.Application.Global.i18n.Cultures.Queries.GetCulture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Areas.Configuration.Pages.Cultures;


public class EditModel : BasePageModel
{
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<CulturesResources> _loc;

    public EditModel(IMapper mapper, IStringLocalizer<CulturesResources> loc)
    {
        _mapper = mapper;
        _loc = loc;

        //Localización ViewData (Se usa en _Layout): No se puede acceder a ViewData en el ctor, pero sí a una propiedad con el atributo ViewData.
        Title = _loc["Editar Cultura"];
    }

    [HiddenInput][BindProperty] public long Id { get; set; }

    [Display(Name = "Nombre")][BindProperty] public string Name { get; set; }

    [Display(Name = "Código")][BindProperty] public string Code { get; set; }

    [Display(Name = "Lenguaje")][BindProperty] public string Language { get; set; }

    [BindProperty] public DateTime Modified { get; set; }

    public async Task<IActionResult> OnGetAsync(long id)
    {
        return await LoadCulture(id);
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CultureDto, EditModel>();
        }
    }

    private async Task<IActionResult> LoadCulture(long id)
    {
        var query = new GetCultureQuery() { Id = id };
        CultureDto cultureDto = await Mediator.Send(query);
        if (cultureDto == null)
        {
            SuccessMessage = _loc["La cultura no existe."];
            return RedirectToPage("/Cultures/Index", new { area = "Configuration" });
        }

        _mapper.Map(cultureDto, this);

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var command = new UpdateCultureCommand()
        {
            Id = Id,
            Name = Name,
            //Code = this.Code,
            //Language = this.Language,
        };

        await Mediator.Send(command);

        SuccessMessage = _loc["Se ha modificado la cultura {0}.", Name];

        return RedirectByModelState("/Cultures/Detail", new { area = "Configuration", id = Id });
    }
}

using AutoMapper;
using GSF.Application.Global.i18n.Cultures.Commands.DeleteCulture;
using GSF.Application.Global.i18n.Cultures.Queries;
using GSF.Application.Global.i18n.Cultures.Queries.GetCulture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Socios.Web.Pages;
using System.ComponentModel.DataAnnotations;

namespace Socios.Web.Areas.Configuration.Pages.Cultures;


public class DetailModel : BasePageModel
{

    private readonly IMapper _mapper;
    private readonly IStringLocalizer<CulturesResources> _loc;

    public DetailModel(IMapper mapper, IStringLocalizer<CulturesResources> loc)
    {
        _mapper = mapper;
        _loc = loc;

        //Localización ViewData (Se usa en _Layout): No se puede acceder a ViewData en el ctor, pero sí a una propiedad con el atributo ViewData.
        Title = _loc["Detalle de Cultura"];
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
            CreateMap<CultureDto, DetailModel>();
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

    public async Task<IActionResult> OnPostDeleteAsync(long Id, string Name, byte[] rowVersion)
    {
        IActionResult result;
        DeleteCultureCommand command = new DeleteCultureCommand() { Id = Id, RowVersion = rowVersion };
        try
        {
            await Mediator.Send(command);
            SuccessMessage = _loc["Se ha eliminado la cultura : {0}", Name];
            result = RedirectByModelState("/Cultures/Index", new { area = "Configuration" });
        }
        catch (DbUpdateConcurrencyException)
        {
            ErrorMessage = _loc["Los datos fueron modificados por otro usuario. Intente nuevamente."];
            result = RedirectToPage("/Cultures/Index", new { area = "Configuration" });
        }
        return result;
    }
}

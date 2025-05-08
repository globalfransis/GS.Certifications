using GSF.Application.Common.Caching;
using GSF.Application.Common.Mappings;
using GSF.Application.Global.i18n.Cultures.Commands.CreateCulture;
using GSF.Application.Global.i18n.Cultures.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Socios.Web.Pages;
using System.ComponentModel.DataAnnotations;

namespace Socios.Web.Areas.Configuration.Pages.Cultures;

public class CreateModel : BasePageModel, IMapFrom<CultureDto>
{

    private readonly IStringLocalizer<CulturesResources> _loc;
    ICache _cache;

    public CreateModel(IStringLocalizer<CulturesResources> loc, ICache cache)
    {
        _loc = loc;
        _cache = cache;

        //Localización ViewData (Se usa en _Layout): No se puede acceder a ViewData en el ctor, pero sí a una propiedad con el atributo ViewData.
        Title = _loc["Crear Cultura"];
    }

    [HiddenInput][BindProperty] public long Id { get; set; }

    [Display(Name = "Nombre")][BindProperty] public string Name { get; set; }

    [Display(Name = "Código")][BindProperty] public string Code { get; set; }

    [Display(Name = "Lenguaje")][BindProperty] public string Language { get; set; }


    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var command = new CreateCultureCommand()
        {
            Name = Name,
            Code = Code,
            Language = Language,
        };

        Id = await Mediator.Send(command);

        //Localización interpolada
        SuccessMessage = _loc["Se ha creado la cultura {0}.", Name];

        return RedirectByModelState("/Cultures/Detail", new { area = "Configuration", id = Id });
    }
}

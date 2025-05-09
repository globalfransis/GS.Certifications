using GSF.Application.Global.i18n.Cultures.Queries;
using GSF.Application.Global.i18n.Cultures.Queries.GetCultures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using GS.Certifications.Web.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Areas.Configuration.Pages.Cultures;

public class IndexModel : BasePageModel
{
    [BindProperty] public List<CultureDto> Cultures { get; private set; }

    [BindProperty] public List<SelectListItem> CulturesOptions { get; private set; }

    [Display(Name = "Nombre")][BindProperty(SupportsGet = true)] public string FilterName { get; set; }

    [Display(Name = "Código")][BindProperty(SupportsGet = true)] public string FilterCode { get; set; }

    [Display(Name = "Idioma")][BindProperty(SupportsGet = true)] public string FilterLanguage { get; set; }

    private readonly IStringLocalizer<CulturesResources> _loc;

    public IndexModel(IStringLocalizer<CulturesResources> loc)
    {
        _loc = loc;
        Title = _loc["Configuraciones Regionales"];
    }
    public async Task OnGet()
    {
        Cultures = new List<CultureDto>();
        await LoadControls();
        await LoadCultures();
    }

    public async Task OnGetSearch()
    {
        await LoadControls();
        await LoadCultures();



    }

    //public async Task<IActionResult> OnPostDeleteAsync(long Id, String Name, DateTime Modified)
    //{
    //    DeleteCultureCommand command = new DeleteCultureCommand() { Id = Id, Modified = Modified };
    //    try
    //    {
    //        await Mediator.Send(command);
    //    }
    //    catch (ErrorValidacionException ex)
    //    {
    //        foreach (var errorMessage in ex.Failures.SelectMany(f => f.Value))
    //        {
    //            ModelState.AddModelError("", ex.Message);
    //        }

    //        await LoadControls();
    //        await LoadCultures();
    //        //return Redirect(HttpContext.Request.Headers["referer"]);
    //        return Page();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        PersistentMessage = _loc["Se ha eliminado la cultura {0}.", Name];

    //    }

    //    return RedirectToPage("/Cultures/Index", new { area = "Configuration" });

    //}



    private async Task LoadControls()
    {
        CulturesOptions = new List<SelectListItem>();

        var query = new GetCulturesQuery()
        {
            Name = DBNull.Value.ToString(),
            Code = DBNull.Value.ToString(),
            Language = DBNull.Value.ToString()
        };

        var CulturesOptionsList = await Mediator.Send(query);

        foreach (var language in CulturesOptionsList.Select(lan => lan.Language).Distinct().OrderBy(lan => lan))
        {
            CulturesOptions.Add(new SelectListItem
            {
                Value = language,
                Text = language
            });
        }
    }

    private async Task LoadCultures()
    {
        var query = new GetCulturesQuery()
        {
            Name = FilterName,
            Code = FilterCode,
            Language = FilterLanguage
        };

        Cultures = (await Mediator.Send(query)).OrderBy(cu => cu.Language).ThenByDescending(cu => cu.Code).ToList();
    }

}

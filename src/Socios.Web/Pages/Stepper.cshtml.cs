using AutoMapper;
using GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Seguridad;
using GSF.Application.Common.Exceptions;
using GSF.Application.Common.Interfaces;
using GSF.Application.Security.SecurityTemp;
using GSF.Application.Security.Services;
using GSF.Domain.Entities.Companies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Socios.Web.Areas.Security.Pages;
using Socios.Web.Common.Services.Empresa.Storage;

namespace Socios.Web.Pages;

[Authorize]
public class StepperModel : BasePageModel
{
    private readonly IMediator _mediator;
    private readonly IStringLocalizer<Login> _loc;
    private readonly IMapper _mapper;
    private readonly IUsuarioEmpresaPortalService _usuarioEmpresaPortalService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISecurityTempStoreService _securityTempStoreService;
    private readonly ICurrentEmpresaPortalRepository _empresaPortalRepository;

    private readonly IWebHostEnvironment _webHostEnvironment;
    public IWebHostEnvironment WebHostEnvironment => _webHostEnvironment;

    [BindProperty]
    public long? CompanyId { get; set; } // Vincula el ID seleccionado del dropdow   

    [BindProperty]
    public List<SecurityUserCompaniesDto> Companies { get; set; }

    [BindProperty]
    public long? UsuarioEmpresaPortalId { get; set; } // Vincula el ID seleccionado del dropdow  


    public StepperModel(
        IMediator mediator,
        IStringLocalizer<Login> loc,
        IMapper mapper,
        IUsuarioEmpresaPortalService usuarioEmpresaPortalService,
        ICurrentUserService currentUserService,
        ISecurityTempStoreService securityTempStoreService,
        ICurrentEmpresaPortalRepository empresaPortalRepository,
        IWebHostEnvironment webHostEnvironment
        )
    {
        _mediator = mediator;
        _loc = loc;
        _mapper = mapper;
        _usuarioEmpresaPortalService = usuarioEmpresaPortalService;
        _currentUserService = currentUserService;
        _securityTempStoreService = securityTempStoreService;
        _empresaPortalRepository = empresaPortalRepository;
        _webHostEnvironment = webHostEnvironment;
        Title = "Stepper";
    }

    public async Task OnGet(string returnUrl)
    {
        await SetComboCompanies();
    }


    public async Task<IActionResult> OnPostAsync()
    {

        await SetComboCompanies();
        if (UsuarioEmpresaPortalId is null) throw new ValidationErrorException("UsuarioEmpresaPortalId", _loc["Debe seleccionar una Empresa Portal."]);

        await SetupContext();
        return RedirectToPage("/Index");
    }

    private async Task SetComboCompanies()
    {
        long userId = _currentUserService.UserId;
        Companies = await _securityTempStoreService.GetUserCompaniesAsync();
        Companies = Companies.OrderBy(c => c.BusinessName).ToList();
    }
    public async Task<IActionResult> OnGetEmpresasPortales(int companyId)
    {
        long userId = _currentUserService.UserId;
        IEnumerable<GS.Certifications.Domain.Entities.Seguridad.UsuarioEmpresaPortal> ueps = await _usuarioEmpresaPortalService.GetAllAsync(new GetAllRequestDto() { UserId = userId });

        var data = ueps
                    .Where(uep => uep.EmpresaPortal.Company.Id == companyId && uep.Habilitado)
                    .OrderBy(uep => uep.EmpresaPortal.CompanyId)
                    .ThenBy(uep => uep.EmpresaPortalId)
                    .Select(uep => new { Key = uep.Id, Value = uep.EmpresaPortal.RazonSocial })
                    .ToList();

        return new JsonResult(data);
    }

    private async Task SetupContext()
    {
        long userId = _currentUserService.UserId;
        IEnumerable<UsuarioEmpresaPortal> ueps = await _usuarioEmpresaPortalService.GetAllAsync(new GetAllRequestDto() { UserId = userId });
        UsuarioEmpresaPortal uep = ueps.Where(uep => uep.Id == UsuarioEmpresaPortalId).SingleOrDefault();

        EmpresaPortal empresaPortal = uep.EmpresaPortal;
        Company company = uep.EmpresaPortal.Company;
        SecurityUserCompaniesDto suc = _mapper.Map<SecurityUserCompaniesDto>(company);

        await _securityTempStoreService.SetCurrentUserCompanyAsync(suc);
        _empresaPortalRepository.SetUsuarioEmpresaPortal(uep);
    }
}

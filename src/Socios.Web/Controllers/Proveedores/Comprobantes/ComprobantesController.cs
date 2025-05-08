using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Queries;
using GS.Certifications.Domain.Entities.Comprobantes;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Socios.Web.Common.Services.Empresa;
using Socios.Web.Controllers.Common.Attributes;

namespace Socios.Web.Controllers.Proveedores.Comprobantes;

[Route("api/Proveedores/[controller]")]
[ApiController]
[AuthorizationGSF]
public class ComprobantesController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentEmpresaPortalService _currentEmpresaPortalService;
    private readonly ICurrentCompanyService _currentCompanyService;
    private readonly IEmpresaPortalService _empresaPortalService;

    public ComprobantesController(IMediator mediator, ICurrentEmpresaPortalService currentEmpresaPortalService, ICurrentCompanyService currentCompanyService, IEmpresaPortalService empresaPortalService)
    {
        _mediator = mediator;
        _currentEmpresaPortalService = currentEmpresaPortalService;
        _currentCompanyService = currentCompanyService;
        _empresaPortalService = empresaPortalService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComprobanteDto>> GetOneAsync([FromRoute] int id)
    {
        var query = new GetComprobanteQuery()
        {
            Id = id
        };

        return await _mediator.Send(query);
    }

    [HttpPost("Analisis")]
    public async Task<ActionResult<int>> AnalyzeAsync()
    {
        if (HttpContext.Request.Form.Files.Count == 0) return BadRequest("Se debe enviar un archivo.");
        var currentCompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();
        var currentEmpresaPortalId = _currentEmpresaPortalService.GetCurrentEmpresaPortalId();
        var cmd = new AnalyzeComprobanteCommand
        {
            CompanyId = currentCompanyId,
            EmpresaId = (int)currentEmpresaPortalId,
            FormFile = HttpContext.Request.Form.Files[0],
            OrigenId = Origen.PROVEEDOR
        };

        return Ok(await _mediator.Send(cmd));
    }


    [HttpDelete("Analisis")]
    public async Task<Unit> Discard([FromBody] DeleteTempComprobanteCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<IPaginatedQueryResult<ComprobanteDto>>> GetAllAsync([FromQuery] GetComprobantesQuery query)
    {
        var currentEmpresaId = _currentEmpresaPortalService.GetCurrentEmpresaPortalId();
        query.EmpresaId = currentEmpresaId;
        return Ok(await _mediator.Send(query));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> UpdateAsync([FromRoute] int id, [FromBody] UpdateComprobanteCommand command)
    {
        var currentEmpresaPortalId = _currentEmpresaPortalService.GetCurrentEmpresaPortalId();
        var currentEmpresaPortal = await _empresaPortalService.GetAsync((long)currentEmpresaPortalId);
        //command.NroIdentificacionFiscalPro = currentEmpresaPortal.IdentificadorTributario;

        // check if we should take it from the user or from the current empresaPortal
        // in the first case we should validate it with the address of the current one
        //command.DomicilioPro = currentEmpresaPortal.Direccion;
        command.CategoriaTipoEmisorId = currentEmpresaPortal.TipoResponsableId;

        // CAREFUL!!! this is de CompanyId, not the CompanyExtraId
        command.CompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();

        command.Id = id;

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<int>> PostAsync([FromBody] CreateComprobanteCommand command)
    {
        var currentEmpresaPortalId = _currentEmpresaPortalService.GetCurrentEmpresaPortalId();
        var currentEmpresaPortal = await _empresaPortalService.GetAsync((long)currentEmpresaPortalId);
        command.NroIdentificacionFiscalPro = currentEmpresaPortal.IdentificadorTributario;
        command.EmpresaId = currentEmpresaPortal.Id;

        // check if we should take it from the user or from the current empresaPortal
        // in the first case we should validate it with the address of the current one
        //command.DomicilioPro = currentEmpresaPortal.Direccion;
        command.CategoriaTipoEmisorId = currentEmpresaPortal.TipoResponsableId;

        // CAREFUL!!! this is de CompanyId, not the CompanyExtraId
        command.CompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();

        return await _mediator.Send(command);
    }


    [HttpPost("Borrador")]
    public async Task<ActionResult<int>> SaveAsync([FromBody] SaveComprobanteDraftCommand command)
    {
        var currentEmpresaPortalId = _currentEmpresaPortalService.GetCurrentEmpresaPortalId();
        var currentEmpresaPortal = await _empresaPortalService.GetAsync((long)currentEmpresaPortalId);
        command.NroIdentificacionFiscalPro = currentEmpresaPortal.IdentificadorTributario;
        command.EmpresaId = currentEmpresaPortal.Id;

        command.CategoriaTipoEmisorId = currentEmpresaPortal.TipoResponsableId;

        command.CompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();

        return await _mediator.Send(command);
    }

    [HttpPut("Borrador/{id}")]
    public async Task<ActionResult<Unit>> UpdateDraftAsync([FromRoute] int id, [FromBody] UpdateComprobanteDraftCommand command)
    {
        var currentEmpresaPortalId = _currentEmpresaPortalService.GetCurrentEmpresaPortalId();
        var currentEmpresaPortal = await _empresaPortalService.GetAsync((long)currentEmpresaPortalId);

        // check if we should take it from the user or from the current empresaPortal
        // in the first case we should validate it with the address of the current one
        //command.DomicilioPro = currentEmpresaPortal.Direccion;
        command.CategoriaTipoEmisorId = currentEmpresaPortal.TipoResponsableId;

        // CAREFUL!!! this is de CompanyId, not the CompanyExtraId
        command.CompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();

        command.Id = id;

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("Constatar")]
    public async Task<ActionResult<Unit>> VerifyAsync([FromBody] VerifyComprobanteCommand command)
    {
        var currentEmpresaPortalId = _currentEmpresaPortalService.GetCurrentEmpresaPortalId();
        var currentEmpresaPortal = await _empresaPortalService.GetAsync((long)currentEmpresaPortalId);
        command.NroIdentificacionFiscalPro = currentEmpresaPortal.IdentificadorTributario;
        command.EmpresaId = currentEmpresaPortal.Id;

        // check if we should take it from the user or from the current empresaPortal
        // in the first case we should validate it with the address of the current one
        //command.DomicilioPro = currentEmpresaPortal.Direccion;
        command.CategoriaTipoEmisorId = currentEmpresaPortal.TipoResponsableId;

        // CAREFUL!!! this is de CompanyId, not the CompanyExtraId
        command.CompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();

        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> DeleteAsync([FromRoute] int id, [FromQuery] byte[] rowVersion)
    {
        var cmd = new DeleteComprobanteCommand() { Id = id, RowVersion = rowVersion };
        return await _mediator.Send(cmd);
    }


    [HttpGet("Estados")]
    public async Task<ActionResult<List<ComprobanteEstadoDto>>> GetAllAsync([FromQuery] GetComprobanteEstadosQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPut("{id}/Aprobar")]
    public async Task<ActionResult<Unit>> ApproveAsync([FromRoute] int id, [FromBody] ApproveComprobanteCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}/Confirmar")]
    public async Task<ActionResult<Unit>> ConfirmAsync([FromRoute] int id, [FromBody] ConfirmComprobanteCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}/Autorizar")]
    public async Task<ActionResult<Unit>> AuthorizeAsync([FromRoute] int id, [FromBody] AuthorizeComprobanteCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}/Rechazar")]
    public async Task<ActionResult<Unit>> RejectAsync([FromRoute] int id, [FromBody] RejectComprobanteCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);
        return Ok();
    }

}

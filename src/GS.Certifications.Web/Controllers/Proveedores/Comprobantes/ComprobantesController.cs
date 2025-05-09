using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Security.Services.CurrentCompany;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Queries;
using GS.Certifications.Web.Controllers.Common.Attributes;
using GS.Certifications.Domain.Entities.Comprobantes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Proveedores.Comprobantes;

[Route("api/Proveedores/[controller]")]
[ApiController]
[AuthorizationGSF]
public class ComprobantesController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICurrentCompanyService _currentCompanyService;

    public ComprobantesController(IMediator mediator, ICurrentCompanyService currentCompanyService)
    {
        _mediator = mediator;
        _currentCompanyService = currentCompanyService;
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
    public async Task<ActionResult<int>> AnalyzeAsync([FromQuery] int EmpresaId)
    {
        if (HttpContext.Request.Form.Files.Count == 0) return BadRequest("Se debe enviar un archivo.");
        var currentCompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();
        var cmd = new AnalyzeComprobanteCommand
        {
            CompanyId = currentCompanyId,
            EmpresaId = EmpresaId,
            FormFile = HttpContext.Request.Form.Files[0],
            OrigenId = Origen.BACKOFFICE
        };

        return Ok(await _mediator.Send(cmd));
    }


    [HttpDelete("Analisis")]
    public async Task<Unit> Discard([FromBody] DeleteTempComprobanteCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<IPaginatedQueryResult<ComprobanteDto>>> GetAllAsync([FromQuery] GetComprobantesCompanyQuery query)
    {
        var currentCompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();
        query.CompanyId = currentCompanyId;

        return Ok(await _mediator.Send(query));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> UpdateAsync([FromRoute] int id, [FromBody] UpdateComprobanteCommand command)
    {
        // CAREFUL!!! this is de CompanyId, not the CompanyExtraId
        command.CompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();

        command.Id = id;

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}/Aprobar")]
    public async Task<ActionResult<Unit>> ApproveAsync([FromRoute] int id, [FromBody] ApproveComprobanteCommand command)
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

    [HttpPut("{id}/Periodos")]
    public async Task<ActionResult<Unit>> UpdatePeriodoAsync([FromRoute] int id, [FromBody] UpdatePeriodoComprobanteCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<int>> PostAsync([FromBody] CreateComprobanteCommand command)
    {
        // CAREFUL!!! this is de CompanyId, not the CompanyExtraId
        command.CompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();

        return await _mediator.Send(command);
    }

    [HttpPost("Borrador")]
    public async Task<ActionResult<int>> SaveAsync([FromBody] SaveComprobanteDraftCommand command)
    {
        command.CompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();

        return await _mediator.Send(command);
    }

    [HttpPut("Borrador/{id}")]
    public async Task<ActionResult<Unit>> UpdateDraftAsync([FromRoute] int id, [FromBody] UpdateComprobanteDraftCommand command)
    {
        // CAREFUL!!! this is de CompanyId, not the CompanyExtraId
        command.CompanyId = await _currentCompanyService.GetCurrentCompanyIdLazyAsync();

        command.Id = id;

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id}/Constatar")]
    public async Task<ActionResult<Unit>> VerifyAsync([FromRoute] int id, [FromBody] VerifyComprobanteCommand command)
    {
        command.Id = id;
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
}

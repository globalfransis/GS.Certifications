using GSF.Application.Alertas;
using GSF.Application.Alertas.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using GS.Certifications.Web.Controllers.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Archivos;

[Route("api/[controller]")]
[ApiController]
[AuthorizationGSF]
public class ArchivosController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public ArchivosController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("ImportarAlertaArchivos")]
    public async Task<List<TargetArchivoDto>> ImportarTarjetaArchivo()
    {
        var command = new ImportarAlertaCommand();
        for (int i = 0; i < HttpContext.Request.Form.Files.Count; i++)
        {
            IFormFile file = new FormFile(null, 0, 0, "", "");
            file = HttpContext.Request.Form.Files[i];
            command.Files.Add(file);
        }
        return await _mediator.Send(command);
    }
}

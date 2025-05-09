using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GS.Certifications.Application.Commons.Dtos.EmailProcessor;
using GS.Certifications.Application.UseCases.EmailProcessor.Commands;
using GS.Certifications.Domain.Commons.Exceptions.EmailProcessor;
using System;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.EmailProcessor
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class EmailProcessorController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmailProcessorController> _logger;

        public EmailProcessorController(IMediator mediator, ILogger<EmailProcessorController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("company/{companyId}/process-mails")]
        public async Task<IActionResult> Post(int companyId, [FromBody] EmailProcessorRequestDto request)
        {
            _logger.LogInformation($"Procesando correos para empresa {companyId} para el proceso {request.ProcesoId}");

            try
            {
                return Ok(await _mediator.Send(new EmailProcessorCommand() { ProcesoId = request.ProcesoId, CompanyId = companyId }));
            }
            catch (CompanyNotAllowedForIntegracionFacturaPorCorreoException ex)
            {
                _logger.LogWarning(ex.ToString());
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }


        }
    }
}

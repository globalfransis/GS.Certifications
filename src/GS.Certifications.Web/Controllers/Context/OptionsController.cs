using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using GSF.Application.Security.Options.Commands;
using System.Threading.Tasks;

namespace GS.Certifications.Web.Controllers.Context
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task Post([FromBody] SetCurrentOptionCommand command)
        {
            _ = await Mediator.Send(new SetPreviousOptionCommand());

            _ = await Mediator.Send(command);
        }
    }
}

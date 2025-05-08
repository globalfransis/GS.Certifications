using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Exceptions;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Common.Exceptions;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands
{
    public class ApproveComprobanteCommand : IRequest<Unit>, IComprobanteApprove
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class ApproveComprobanteCommandHandler : BaseRequestHandler<Unit, ApproveComprobanteCommand, Unit>
    {
        private readonly ICertificationsDbContext Context;
        private readonly IComprobanteService comprobanteService;

        public ApproveComprobanteCommandHandler(ICertificationsDbContext context, IComprobanteService comprobanteService)
        {
            Context = context;
            this.comprobanteService = comprobanteService;
        }

        protected override async Task<Unit> HandleRequestAsync(ApproveComprobanteCommand request, CancellationToken cancellationToken)
        {
            try
            {

                await comprobanteService.ApproveAsync(request.Id, request);
                await Context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (ComprobanteYaFueAprobadoException ex)
            {
                throw new ValidationErrorException();
            }
            catch
            {
                throw;
            }
        }
    }
}

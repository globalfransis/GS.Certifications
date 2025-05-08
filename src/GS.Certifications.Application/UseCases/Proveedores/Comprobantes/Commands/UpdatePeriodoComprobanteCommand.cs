using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Common.Exceptions;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands
{
    public class UpdatePeriodoComprobanteCommand : IRequest<Unit>, IComprobantePeriodoUpdate
    {
        public int Id { get; set; }
        public int? PeriodoId { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class UpdatePeriodoComprobanteCommandHandler : BaseRequestHandler<Unit, UpdatePeriodoComprobanteCommand, Unit>
    {
        private readonly ICertificationsDbContext Context;
        private readonly IComprobanteService comprobanteService;

        public UpdatePeriodoComprobanteCommandHandler(ICertificationsDbContext context, IComprobanteService comprobanteService)
        {
            Context = context;
            this.comprobanteService = comprobanteService;
        }

        protected override async Task<Unit> HandleRequestAsync(UpdatePeriodoComprobanteCommand request, CancellationToken cancellationToken)
        {
            try
            {

                await comprobanteService.UpdatePeriodoAsync(request.Id, request);
                await Context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (ArgumentNullException ex)
            {
                throw new ValidationErrorException("Periodo", "Se debe especificar un período.");
            }
            catch
            {
                throw;
            }
        }
    }
}

using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Exceptions;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GSF.Application.Common.Exceptions;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands
{
    public class DeleteSolicitudCertificacionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public short OrigenEliminacionId { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class DeleteSolicitudCertificacionCommandHandler : BaseRequestHandler<Unit, DeleteSolicitudCertificacionCommand, Unit>
    {
        private readonly ICertificationsDbContext Context;

        private readonly ICertificacionService certificacionService;
        public DeleteSolicitudCertificacionCommandHandler(ICertificationsDbContext context, ICertificacionService certificacionService)
        {
            Context = context;
            this.certificacionService = certificacionService;
        }

        protected override async Task<Unit> HandleRequestAsync(DeleteSolicitudCertificacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await certificacionService.DeleteSolicitudAsync(request.Id, request.OrigenEliminacionId, request.RowVersion);
                await Context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (SolicitudEliminacionEstadoInvalidoException ex)
            {
                throw new ValidationErrorException("CertificacionId", ex.Message);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}

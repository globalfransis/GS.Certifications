using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Commands
{
    public class DeleteDocumentoSolicitudCertificacionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class DeleteDocumentoSolicitudCertificacionCommandHandler : BaseRequestHandler<Unit, DeleteDocumentoSolicitudCertificacionCommand, Unit>
    {
        private readonly ICertificationsDbContext Context;

        private readonly ICertificacionService certificacionService;
        public DeleteDocumentoSolicitudCertificacionCommandHandler(ICertificationsDbContext context, ICertificacionService certificacionService)
        {
            Context = context;
            this.certificacionService = certificacionService;
        }

        protected override async Task<Unit> HandleRequestAsync(DeleteDocumentoSolicitudCertificacionCommand request, CancellationToken cancellationToken)
        {
            await certificacionService.DeleteDocumentoSolicitudAsync(request.Id, request.RowVersion);
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

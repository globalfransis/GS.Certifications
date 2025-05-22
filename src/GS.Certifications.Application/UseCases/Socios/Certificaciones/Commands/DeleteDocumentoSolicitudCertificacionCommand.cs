using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using static GS.Certifications.Application.UseCases.Socios.Certificaciones.Services.CertificacionService;

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

            var documentoEliminado = await certificacionService.GetDocumentoAsync(request.Id);

            var nuevoDocCreate = new SolicitudCertificacionDocumentoCreate()
            {
                SolicitudId = documentoEliminado.SolicitudId,
                Version = documentoEliminado.Version != null ? documentoEliminado.Version : 1, // regla: al eliminar un documento, creamos uno nuevo en estado pendiente y con el mismo nro de version
                DocumentoRequeridoId = documentoEliminado.DocumentoRequeridoId
            };

            var nuevoDocumentoPendiente = certificacionService.CreateDocumento(nuevoDocCreate);

            Context.DocumentoCargados.Add(nuevoDocumentoPendiente);

            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

using AutoMapper;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Dto;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GS.Certifications.Domain.Entities.Certificaciones;
using GS.Certifications.Domain.Entities.Certificaciones.Documentos;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.SolicitudCertificacions.Queries;

public class GetSolicitudCertificacionDocumentoQuery : IRequest<DocumentoCargadoDto>
{
    public int Id { get; set; }
}

public class GetSolicitudCertificacionDocumentoQueryHandler : BaseRequestHandler<DocumentoCargado, GetSolicitudCertificacionDocumentoQuery, DocumentoCargadoDto>
{
    private readonly ICertificacionService _service;

    public GetSolicitudCertificacionDocumentoQueryHandler(ICertificacionService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected override async Task<DocumentoCargado> HandleRequestAsync(GetSolicitudCertificacionDocumentoQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetDocumentoAsync(request.Id);
    }
}

using AutoMapper;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Dto;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GS.Certifications.Domain.Entities.Certificaciones;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.SolicitudCertificacions.Queries;

public class GetSolicitudCertificacionQuery : IRequest<SolicitudCertificacionDto>
{
    public int Id { get; set; }
}

public class GetSolicitudCertificacionQueryHandler : BaseRequestHandler<SolicitudCertificacion, GetSolicitudCertificacionQuery, SolicitudCertificacionDto>
{
    private readonly ICertificacionService _service;

    public GetSolicitudCertificacionQueryHandler(ICertificacionService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected override async Task<SolicitudCertificacion> HandleRequestAsync(GetSolicitudCertificacionQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetSolicitudAsync(request.Id);
    }
}

using AutoMapper;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Dto;
using GS.Certifications.Application.UseCases.Socios.Certificaciones.Services;
using GS.Certifications.Domain.Entities.Certificaciones;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Socios.Certificaciones.Queries;

public class GetSolicitudCertificacionesQuery : BaseQueryWithPagination<SolicitudCertificacionDto>, ISolicitudCertificacionQueryParameter
{
    public int? SocioId { get; set; }
    public int? TipoSocioId { get; set; }
    public string Nombre { get; set; }
    public bool? Activa { get; set; }
    public short? EstadoId { get; set; }
    public bool? InMemory { get; set; }
    public int? CertificacionId { get; set; }
}

public class GetSolicitudCertificacionesQueryHandler : BaseRequestHandler<IPaginatedQueryResult<SolicitudCertificacion>, GetSolicitudCertificacionesQuery, IPaginatedQueryResult<SolicitudCertificacionDto>>
{
    private readonly ICertificacionService service;

    public GetSolicitudCertificacionesQueryHandler(ICertificacionService service, IMapper mapper) : base(mapper)
    {
        this.service = service;
    }

    protected override async Task<IPaginatedQueryResult<SolicitudCertificacion>> HandleRequestAsync(GetSolicitudCertificacionesQuery request, CancellationToken cancellationToken)
    {
        return await service.GetSolicitudesAsync(request);
    }
}

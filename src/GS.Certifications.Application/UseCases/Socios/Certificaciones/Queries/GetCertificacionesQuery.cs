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

public class GetCertificacionesQuery : BaseQueryWithPagination<CertificacionDto>, ICertificacionQueryParameter
{
    public int? SocioId { get; set; }
    public int? TipoSocioId { get; set; }
    public string Nombre { get; set; }
    public bool? Activa { get; set; }
    public short? EstadoId { get; set; }
    public bool? InMemory { get; set; }
}

public class GetCertificacionesQueryHandler : BaseRequestHandler<IPaginatedQueryResult<Certificacion>, GetCertificacionesQuery, IPaginatedQueryResult<CertificacionDto>>
{
    private readonly ICertificacionService service;

    public GetCertificacionesQueryHandler(ICertificacionService service, IMapper mapper) : base(mapper)
    {
        this.service = service;
    }

    protected override async Task<IPaginatedQueryResult<Certificacion>> HandleRequestAsync(GetCertificacionesQuery request, CancellationToken cancellationToken)
    {

        return await service.GetCertificacionesAsync(request);
    }
}

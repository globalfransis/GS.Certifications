using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Percepciones;
using GS.Certifications.Application.UseCases.Percepciones.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.Percepciones;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Percepciones.Queries;
// Query definition with pagination
public class GetManyPercepcionesQuery : BaseQueryWithPagination<PercepcionForListDto>, IPercepcionQueryParameter // Replace with the appropriate interface
{
    public string Descripcion { get; set; }
    public int? PercepcionTipoId { get; set; }
    public int? ProvinciaId { get; set; }
    public bool? InMemory { get; set; }
}

// Handler definition for paginated query
public class GetManyPercepcionesQueryHandler : BaseRequestHandler
    <IPaginatedQueryResult<Percepcion>, GetManyPercepcionesQuery, IPaginatedQueryResult<PercepcionForListDto>>
{
    private readonly IPercepcionService service;

    public GetManyPercepcionesQueryHandler(IPercepcionService service, IMapper mapper) : base(mapper)
    {
        this.service = service;
    }

    protected override async Task<IPaginatedQueryResult<Percepcion>>
        HandleRequestAsync(GetManyPercepcionesQuery request, CancellationToken cancellationToken)
    {
        return await service.GetManyAsync(request);
    }
}
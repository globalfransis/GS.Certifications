using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.ConceptosGastosTipos;
using GS.Certifications.Application.UseCases.ConceptosGastosTipos.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.ConceptosGastosTipos;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.ConceptosGastosTipos.Queries;
// Query definition with pagination
public class GetConceptosGastosTiposQuery : BaseQueryWithPagination<ConceptoGastoTipoForListDto>, IConceptoGastoTipoQueryParameter // Replace with the appropriate interface
{
    public string Nombre { get; set; }
    public bool? InMemory { get; set; }
}

// Handler definition for paginated query
public class GetConceptosGastosTiposQueryHandler : BaseRequestHandler
    <IPaginatedQueryResult<ConceptoGastoTipo>, GetConceptosGastosTiposQuery, IPaginatedQueryResult<ConceptoGastoTipoForListDto>>
{
    private readonly IConceptoGastoTipoService service;

    public GetConceptosGastosTiposQueryHandler(IConceptoGastoTipoService service, IMapper mapper) : base(mapper)
    {
        this.service = service;
    }

    protected override async Task<IPaginatedQueryResult<ConceptoGastoTipo>> HandleRequestAsync(GetConceptosGastosTiposQuery request, CancellationToken cancellationToken)
    {
        return await service.GetManyAsync(request);
    }
}
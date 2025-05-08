using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GS.Certifications.Domain.Entities.Impuestos;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Queries;
// Query definition with pagination
public class GetManyImpuestosQuery : BaseQueryWithPagination
    <ImpuestoForListDto>, IImpuestoQueryParameter // Replace with the appropriate interface
{
    public string CodigoAFIP { get; set; }
    public decimal? Valor { get; set; }
    public int? TipoId { get; set; }
    public int? ProvinciaId { get; set; }
    public bool? InMemory { get; set; }
}

// Handler definition for paginated query
public class GetManyImpuestosQueryHandler : BaseRequestHandler<IPaginatedQueryResult<Impuesto>, GetManyImpuestosQuery, IPaginatedQueryResult<ImpuestoForListDto>>
{
    private readonly IImpuestoService service;

    public GetManyImpuestosQueryHandler(IImpuestoService service, IMapper mapper) : base(mapper)
    {
        this.service = service;
    }

    protected override async Task<IPaginatedQueryResult<Impuesto>>
        HandleRequestAsync(GetManyImpuestosQuery request, CancellationToken cancellationToken)
    {
        return await service.GetManyAsync(request);
    }
}
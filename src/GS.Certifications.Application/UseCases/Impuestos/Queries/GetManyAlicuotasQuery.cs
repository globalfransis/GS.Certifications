using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Alicuotas;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.Alicuotas;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Queries;
// Query definition with pagination
public class GetManyAlicuotasQuery : BaseQueryWithPagination<AlicuotaDto>, IAlicuotasQueryParameter // Replace with the appropriate interface
{
    public short Idm { get; set; }
    public string CodigoAFIP { get; set; }
    public decimal? Valor { get; set; }
    public bool? InMemory { get; set; }
}

// Handler definition for paginated query
public class GetManyAlicuotasQueryHandler : BaseRequestHandler<IPaginatedQueryResult<Alicuota>, GetManyAlicuotasQuery, IPaginatedQueryResult<AlicuotaDto>>
{
    private readonly IImpuestoService service;

    public GetManyAlicuotasQueryHandler(IImpuestoService service, IMapper mapper) : base(mapper)
    {
        this.service = service;
    }

    protected override async Task<IPaginatedQueryResult<Alicuota>> HandleRequestAsync(GetManyAlicuotasQuery request, CancellationToken cancellationToken)
    {
        return await service.GetManyAlicuotasAsync(request);
    }
}
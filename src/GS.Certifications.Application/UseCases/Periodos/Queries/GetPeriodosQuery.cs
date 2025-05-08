using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Periodos;
using GS.Certifications.Application.UseCases.Periodos.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GS.Certifications.Domain.Entities.Periodos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Periodos.Queries;
// Query definition with pagination
public class GetPeriodosQuery : BaseQueryWithPagination<PeriodoForListDto>, IPeriodoQueryParameter // Replace with the appropriate interface
{
    public int? EstadoIdm { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public bool? InMemory { get; set; }
}

// Handler definition for paginated query
public class GetPeriodosQueryHandler : BaseRequestHandler<IPaginatedQueryResult<Periodo>, GetPeriodosQuery, IPaginatedQueryResult<PeriodoForListDto>>
{
    private readonly IPeriodoService service;

    public GetPeriodosQueryHandler(IPeriodoService service, IMapper mapper) : base(mapper)
    {
        this.service = service;
    }

    protected override async Task<IPaginatedQueryResult<Periodo>> HandleRequestAsync(GetPeriodosQuery request, CancellationToken cancellationToken)
    {
        return await service.GetManyAsync(request);
    }
}
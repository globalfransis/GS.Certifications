using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Periodos;
using GS.Certifications.Application.UseCases.Periodos.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Domain.Entities.Periodos;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Periodos.Queries;

// Query definition
public class GetPeriodoQuery : IRequest<PeriodoDto>
{
    public int Id { get; set; }
}

// Query handler definition
public class GetPeriodoQueryHandler : BaseRequestHandler<Periodo, GetPeriodoQuery, PeriodoDto>
{
    private readonly IPeriodoService _service;

    public GetPeriodoQueryHandler(IPeriodoService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected override async Task<Periodo> HandleRequestAsync(GetPeriodoQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAsync(request.Id);
    }
}

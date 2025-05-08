using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Percepciones;
using GS.Certifications.Application.UseCases.Percepciones.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Domain.Entities.Percepciones;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Percepciones.Queries;

// Query definition
public class GetPercepcionQuery : IRequest<PercepcionBackendDto>
{
    public int Id { get; set; }
}

// Query handler definition
public class GetPercepcionQueryHandler : BaseRequestHandler<Percepcion, GetPercepcionQuery, PercepcionBackendDto>
{
    private readonly IPercepcionService _service;

    public GetPercepcionQueryHandler(IPercepcionService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected override async Task<Percepcion> HandleRequestAsync
        (GetPercepcionQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAsync(request.Id);
    }
}

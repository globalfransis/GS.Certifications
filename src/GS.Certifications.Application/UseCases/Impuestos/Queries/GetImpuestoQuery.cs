using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using GS.Certifications.Domain.Entities.Impuestos;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Queries;

// Query definition
public class GetImpuestoQuery : IRequest<ImpuestoBackendDto>
{
    public int Id { get; set; }
}

// Query handler definition
public class GetImpuestoQueryHandler : BaseRequestHandler<Impuesto, GetImpuestoQuery, ImpuestoBackendDto>
{
    private readonly IImpuestoService _service;

    public GetImpuestoQueryHandler(IImpuestoService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected override async Task<Impuesto> HandleRequestAsync
        (GetImpuestoQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAsync(request.Id);
    }
}

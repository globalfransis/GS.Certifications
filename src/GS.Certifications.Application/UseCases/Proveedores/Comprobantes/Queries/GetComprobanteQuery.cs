using AutoMapper;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Dto;
using GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Domain.Entities.Comprobantes;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Proveedores.Comprobantes.Queries;

public class GetComprobanteQuery : IRequest<ComprobanteDto>
{
    public int Id { get; set; }
}

public class GetComprobanteQueryHandler : BaseRequestHandler<Comprobante, GetComprobanteQuery, ComprobanteDto>
{
    private readonly IComprobanteService _service;

    public GetComprobanteQueryHandler(IComprobanteService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected override async Task<Comprobante> HandleRequestAsync(GetComprobanteQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAsync(request.Id);
    }
}

using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Queries;

// Query definition
public class GetOrdenCompraQuery : IRequest<OrdenCompraDto>
{
    public int Id { get; set; }
}

// Query handler definition
public class GetOrdenCompraQueryHandler : BaseRequestHandler<OrdenCompra, GetOrdenCompraQuery, OrdenCompraDto>
{
    private readonly IOrdenCompraService _service;

    public GetOrdenCompraQueryHandler(IOrdenCompraService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected override async Task<OrdenCompra> HandleRequestAsync(GetOrdenCompraQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAsync(request.Id);
    }
}

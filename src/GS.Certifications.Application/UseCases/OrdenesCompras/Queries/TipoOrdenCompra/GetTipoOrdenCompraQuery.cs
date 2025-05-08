using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Queries.TipoOrdenCompra;

// Query definition
public class GetTipoOrdenCompraQuery : IRequest<OrdenCompraTipoDto>
{
    public int Id { get; set; }
}

// Query handler definition
public class GetTiposOrdenCompraQueryHandler : BaseRequestHandler<OrdenCompraTipo, GetTipoOrdenCompraQuery, OrdenCompraTipoDto>
{
    private readonly IOrdenCompraService _service;

    public GetTiposOrdenCompraQueryHandler(IOrdenCompraService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected override async Task<OrdenCompraTipo> HandleRequestAsync(GetTipoOrdenCompraQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetTipoAsync(request.Id);
    }
}

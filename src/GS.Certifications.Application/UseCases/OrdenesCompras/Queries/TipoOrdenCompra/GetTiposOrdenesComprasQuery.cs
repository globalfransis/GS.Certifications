using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Queries.TipoOrdenCompra;
// Query definition with pagination
public class GetTiposOrdenesComprasQuery : BaseQueryWithPagination<OrdenCompraTipoForListDto>, IOrdenCompraTipoQueryParameter // Replace with the appropriate interface
{
    public string Nombre { get; set; }
    public int? Caracteristica { get; set; }
    public bool? InMemory { get; set; }
}

// Handler definition for paginated query
public class GetTiposOrdenesComprasQueryHandler : BaseRequestHandler
    <IPaginatedQueryResult<OrdenCompraTipo>, GetTiposOrdenesComprasQuery, IPaginatedQueryResult<OrdenCompraTipoForListDto>>
{
    private readonly IOrdenCompraService service;

    public GetTiposOrdenesComprasQueryHandler(IOrdenCompraService service, IMapper mapper) : base(mapper)
    {
        this.service = service;
    }

    protected override async Task<IPaginatedQueryResult<OrdenCompraTipo>> HandleRequestAsync(GetTiposOrdenesComprasQuery request, CancellationToken cancellationToken)
    {
        return await service.GetManyTiposAsync(request);
    }
}
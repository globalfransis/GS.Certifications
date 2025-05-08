using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.OrdenesCompras;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Queries;
// Query definition with pagination
public class GetOrdenesComprasQuery : BaseQueryWithPagination<OrdenCompraForListDto>, IOrdenCompraQueryParameter // Replace with the appropriate interface
{
    public int? EmpresaPortalId { get; set; }
    public DateTime? FechaDesde { get; set; }
    public DateTime? FechaHasta { get; set; }
    public int? OrdenCompraTipoId { get; set; }
    public int? OrdenCompraEstadoIdm { get; set; }
    public bool? InMemory { get; set; }
}

// Handler definition for paginated query
public class GetOrdenesComprasQueryHandler : BaseRequestHandler
    <IPaginatedQueryResult<OrdenCompra>, GetOrdenesComprasQuery, IPaginatedQueryResult<OrdenCompraForListDto>>
{
    private readonly IOrdenCompraService _service;

    public GetOrdenesComprasQueryHandler(IOrdenCompraService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected async override Task<IPaginatedQueryResult<OrdenCompra>> HandleRequestAsync(GetOrdenesComprasQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetManyAsync(request);
    }
}
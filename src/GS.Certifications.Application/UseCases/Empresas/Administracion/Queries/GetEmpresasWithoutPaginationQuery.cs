using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Helpers.Pagination.Enums;
using GSF.Application.Helpers.Pagination.Interfaces;
using MediatR;
using GS.Certifications.Domain.Entities.Empresas;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;

public class GetEmpresasWithoutPaginationQuery : IRequest<List<EmpresaPortalDto>>, IEmpresasQueryParameter
{
    public string RazonSocial { get; set; }
    public string NombreFantasia { get; set; }
    public string IdentificadorTributario { get; set; }
    public bool? GranContribuyente { get; set; }
    public string Contacto { get; set; }
    public long? PaisId { get; set; }
    public long? ProvinciaId { get; set; }
    public long? CiudadId { get; set; }
    public bool? InMemory { get; set; }
    public string OrderPropertyName { get; set; } = nameof(EmpresaPortal.NombreFantasia);
    public OrderDirection OrderDirection { get; set; } = OrderDirection.ASC;
    public int Start { get; set; } = 0;
    public int? Length { get; set; } = null;
    public List<IQuerySortable> SortOrders { get; set; }
}

public class GetEmpresasWithoutPaginationQueryHandler : BaseRequestHandler<List<EmpresaPortal>, GetEmpresasWithoutPaginationQuery, List<EmpresaPortalDto>>
{
    private readonly IEmpresaPortalService empresaPortalService;

    public GetEmpresasWithoutPaginationQueryHandler(IEmpresaPortalService empresaPortalService, IMapper mapper) : base(mapper)
    {
        this.empresaPortalService = empresaPortalService;
    }

    protected override async Task<List<EmpresaPortal>> HandleRequestAsync(GetEmpresasWithoutPaginationQuery request, CancellationToken cancellationToken)
    {
        // TODO: we MUST filter this query by those companies that the current user has permission to view
        // i.e., those that have a UserEmpresa relationship with the current user
        var empresasPaginationResult = await empresaPortalService.GetManyAsync(request);
        return empresasPaginationResult.List;
    }

    protected override GetEmpresasWithoutPaginationQuery PreHandleInvoke(GetEmpresasWithoutPaginationQuery request)
    {
        // we intercept the request to avoid any modifications that may have been made to the request
        request.Length = null;
        request.OrderPropertyName = nameof(EmpresaPortal.NombreFantasia);
        request.OrderDirection = OrderDirection.ASC;
        request.Start = default;
        request.SortOrders = null;

        return base.PreHandleInvoke(request);
    }
}

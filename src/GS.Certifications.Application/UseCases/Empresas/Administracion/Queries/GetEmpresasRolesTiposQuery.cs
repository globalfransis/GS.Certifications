using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;
// Query definition with pagination
public class GetEmpresasRolesTiposQuery : BaseQueryWithPagination<EmpresaRolDto>, IEmpresasRolesQueryParameter // Replace with the appropriate interface
{
    public int? EmpresaPortalId { get; set; }
    public bool? InMemory { get; set; }
}

// Handler definition for paginated query
public class GetEmpresasRolesTiposQueryHandler : BaseRequestHandler
    <IPaginatedQueryResult<EmpresaRol>, GetEmpresasRolesTiposQuery, IPaginatedQueryResult<EmpresaRolDto>>
{
    private readonly IEmpresaPortalService service;

    public GetEmpresasRolesTiposQueryHandler(IEmpresaPortalService service, IMapper mapper) : base(mapper)
    {
        this.service = service;
    }

    protected override async Task<IPaginatedQueryResult<EmpresaRol>>
        HandleRequestAsync(GetEmpresasRolesTiposQuery request, CancellationToken cancellationToken)
    {
        return await service.GetEmpresasRolesManyAsync(request);
    }
}
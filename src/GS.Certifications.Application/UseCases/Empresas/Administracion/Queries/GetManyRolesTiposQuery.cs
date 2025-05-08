using AutoMapper;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Empresas.Roles;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries
{
    // Query definition with pagination
    public class GetManyRolesTiposQuery : BaseQueryWithPagination<RolTipoDto>, IRolesTiposQueryParameter // Replace with the appropriate interface
    {
        public int? EmpresaPortalId { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public bool? InMemory { get; set; }
    }

    // Handler definition for paginated query
    public class GetManyRolesTiposQueryHandler : BaseRequestHandler<IPaginatedQueryResult<RolTipo>, GetManyRolesTiposQuery, IPaginatedQueryResult<RolTipoDto>>
    {
        private readonly IEmpresaPortalService service;

        public GetManyRolesTiposQueryHandler(IEmpresaPortalService service, IMapper mapper) : base(mapper)
        {
            this.service = service;
        }

        protected override async Task<IPaginatedQueryResult<RolTipo>> HandleRequestAsync(GetManyRolesTiposQuery request, CancellationToken cancellationToken)
        {
            return await service.GetManyRolesTiposAsync(request);
        }
    }
}
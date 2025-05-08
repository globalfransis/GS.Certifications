using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Extensions.GSFMediatR.Pagination.Request;
using GSF.Application.Helpers.Pagination.Interfaces;
using GSF.Application.Services;
using GS.Certifications.Domain.Entities.Empresas;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries
{
    // Query definition with pagination
    public class GetEmpresasQuery : BaseQueryWithPagination<EmpresaForListDto>,
        IEmpresasQueryParameter // Replace with the appropriate interface
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
    }

    // Handler definition for paginated query
    public class GetEmpresasQueryHandler : BaseRequestHandler<IPaginatedQueryResult<EmpresaPortal>, GetEmpresasQuery, IPaginatedQueryResult<EmpresaForListDto>>
    {
        private readonly IEmpresaPortalService service;

        public GetEmpresasQueryHandler(IEmpresaPortalService service, IMapper mapper) : base(mapper)
        {
            this.service = service;
        }

        protected override async Task<IPaginatedQueryResult<EmpresaPortal>>
            HandleRequestAsync(GetEmpresasQuery request, CancellationToken cancellationToken)
        {
            return await service.GetManyAsync(request);
        }
    }
}
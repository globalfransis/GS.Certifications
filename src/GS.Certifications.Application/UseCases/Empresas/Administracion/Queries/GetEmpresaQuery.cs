using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Domain.Entities.Empresas;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries
{
    // Query definition
    public class GetEmpresaQuery : IRequest<EmpresaPortalDto>
    {
        public long Id { get; set; }
    }

    // Query handler definition
    public class GetEmpresaQueryHandler : BaseRequestHandler<EmpresaPortal, GetEmpresaQuery, EmpresaPortalDto>
    {
        private readonly IEmpresaPortalService _service;

        public GetEmpresaQueryHandler(IEmpresaPortalService service, IMapper mapper) : base(mapper)
        {
            _service = service;
        }

        protected override async Task<EmpresaPortal> HandleRequestAsync(GetEmpresaQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAsync(request.Id);
        }
    }
}

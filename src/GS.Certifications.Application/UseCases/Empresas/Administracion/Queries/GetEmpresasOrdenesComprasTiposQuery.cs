using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries
{
    public class GetEmpresasOrdenesComprasTiposQuery : IRequest<List<EmpresaOrdenCompraTipoDto>>
    {
        public long? EmpresaPortalId { get; set; }

        public class GetEmpresasOrdenesComprasTiposQueryHandler : IRequestHandler<GetEmpresasOrdenesComprasTiposQuery, List<EmpresaOrdenCompraTipoDto>>
        {
            private readonly IMapper _mapper;
            private readonly IEmpresaPortalService _service;
            public GetEmpresasOrdenesComprasTiposQueryHandler(IMapper mapper, IEmpresaPortalService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<EmpresaOrdenCompraTipoDto>> Handle(GetEmpresasOrdenesComprasTiposQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllEmpresasOrdenesComprasTipos(request.EmpresaPortalId);

                var coleccion = _mapper.Map<List<EmpresaOrdenCompraTipoDto>>(query);

                return coleccion;
            }
        }
    }
}

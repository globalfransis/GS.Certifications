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
    public class GetEmpresasConceptosGastosTiposQuery : IRequest<List<EmpresaConceptoGastoTipoDto>>
    {
        public long? EmpresaPortalId { get; set; }

        public class GetEmpresasConceptosGastosTiposQueryHandler : IRequestHandler<GetEmpresasConceptosGastosTiposQuery, List<EmpresaConceptoGastoTipoDto>>
        {
            private readonly IMapper _mapper;
            private readonly IEmpresaPortalService _service;
            public GetEmpresasConceptosGastosTiposQueryHandler(IMapper mapper, IEmpresaPortalService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<EmpresaConceptoGastoTipoDto>> Handle(GetEmpresasConceptosGastosTiposQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllEmpresasConceptosGastosTipos(request.EmpresaPortalId);

                var coleccion = _mapper.Map<List<EmpresaConceptoGastoTipoDto>>(query);

                return coleccion;
            }
        }
    }
}

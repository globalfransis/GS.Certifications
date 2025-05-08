using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using MediatR;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Queries
{
    public class GetImpuestosTiposQuery : IRequest<List<ImpuestoTipoDto>>
    {
        public class GetImpuestosTiposQueryHandler : IRequestHandler<GetImpuestosTiposQuery, List<ImpuestoTipoDto>>
        {
            private readonly IMapper _mapper;
            private readonly IImpuestoService _service;
            public GetImpuestosTiposQueryHandler
                (IMapper mapper, IImpuestoService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<ImpuestoTipoDto>> Handle
                (GetImpuestosTiposQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllImpuestosTiposCuentas();

                var coleccion = _mapper.Map<List<ImpuestoTipoDto>>(query);

                return coleccion;
            }
        }
    }
}

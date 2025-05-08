using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Percepciones;
using GS.Certifications.Application.UseCases.Percepciones.Services;
using MediatR;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using GS.Certifications.Application.UseCases.Impuestos.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Percepciones.Queries
{
    public class GetPercepcionesTiposQuery : IRequest<List<PercepcionTipoDto>>
    {
        public class GetPercepcionesTiposQueryHandler : IRequestHandler<GetPercepcionesTiposQuery, List<PercepcionTipoDto>>
        {
            private readonly IMapper _mapper;
            private readonly IPercepcionService _service;
            public GetPercepcionesTiposQueryHandler
                (IMapper mapper, IPercepcionService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<PercepcionTipoDto>> Handle
                (GetPercepcionesTiposQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllPercepcionesTiposCuentas();

                var coleccion = _mapper.Map<List<PercepcionTipoDto>>(query);

                return coleccion;
            }
        }
    }
}

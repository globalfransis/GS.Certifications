using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Alicuotas;
using GS.Certifications.Application.UseCases.Impuestos.Services;
using MediatR;
using GS.Certifications.Application.Commons.Dtos.Impuestos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Impuestos.Queries
{
    public class GetAlicuotasQuery : IRequest<List<AlicuotaDto>>
    {
        public class GetAlicuotasQueryHandler :
            IRequestHandler<GetAlicuotasQuery, List<AlicuotaDto>>
        {
            private readonly IMapper _mapper;
            private readonly IImpuestoService _service;
            public GetAlicuotasQueryHandler
                (IMapper mapper, IImpuestoService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<AlicuotaDto>> Handle
                (GetAlicuotasQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllAlicuotasCuentas();

                var coleccion = _mapper.Map<List<AlicuotaDto>>(query);

                return coleccion;
            }
        }
    }
}

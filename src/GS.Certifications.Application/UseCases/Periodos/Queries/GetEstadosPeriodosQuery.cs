using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Periodos;
using GS.Certifications.Application.UseCases.Periodos.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Periodos.Queries;

// Query definition
public class GetEstadosPeriodosQuery : IRequest<List<EstadoPeriodoDto>>
{
    public class GetEstadosPeriodosQueryHandler : IRequestHandler<GetEstadosPeriodosQuery, List<EstadoPeriodoDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPeriodoService _service;
        public GetEstadosPeriodosQueryHandler
            (IMapper mapper, IPeriodoService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<List<EstadoPeriodoDto>> Handle
            (GetEstadosPeriodosQuery request, CancellationToken cancellationToken)
        {
            var query = await _service.GetAllEstadosPeriodos();

            var coleccion = _mapper.Map<List<EstadoPeriodoDto>>(query);

            return coleccion;
        }
    }
}

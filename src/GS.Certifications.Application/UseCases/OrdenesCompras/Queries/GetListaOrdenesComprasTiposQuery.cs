using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Queries
{
    public class GetListaOrdenesComprasTiposQuery : IRequest<List<OrdenCompraTipoDto>>
    {
        public class GetListaOrdenesComprasTiposQueryHandler : IRequestHandler<GetListaOrdenesComprasTiposQuery, List<OrdenCompraTipoDto>>
        {
            private readonly IMapper _mapper;
            private readonly IOrdenCompraService _service;
            public GetListaOrdenesComprasTiposQueryHandler(IMapper mapper, IOrdenCompraService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<OrdenCompraTipoDto>> Handle(GetListaOrdenesComprasTiposQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllOrdenesComprasTipos();

                var coleccion = _mapper.Map<List<OrdenCompraTipoDto>>(query);

                return coleccion;
            }
        }
    }
}

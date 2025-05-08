using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.OrdenesCompras.Services;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.Commons.Dtos.ModosLecturas;
using GS.Certifications.Application.CQRS.DbContexts;
using GS.Certifications.Application.UseCases.CategoriaTipos.Queries;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GS.Certifications.Domain.Entities.Comprobantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.OrdenesCompras.Queries
{
    public class GetListaOrdenesComprasEstadosQuery : IRequest<List<OrdenCompraEstadoDto>>
    {
        public class GetListaOrdenesComprasEstadosQueryHandler : IRequestHandler<GetListaOrdenesComprasEstadosQuery, List<OrdenCompraEstadoDto>>
        {
            private readonly IMapper _mapper;
            private readonly IOrdenCompraService _service;
            public GetListaOrdenesComprasEstadosQueryHandler(IMapper mapper, IOrdenCompraService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<OrdenCompraEstadoDto>> Handle(GetListaOrdenesComprasEstadosQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllOrdenesComprasEstados();

                var coleccion = _mapper.Map<List<OrdenCompraEstadoDto>>(query);

                return coleccion;
            }
        }
    }
}

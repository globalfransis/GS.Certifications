using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.ConceptosGastosTipos;
using GS.Certifications.Application.UseCases.ConceptosGastosTipos.Services;
using MediatR;
using GS.Certifications.Application.Commons.Dtos.OrdenesCompra;
using GS.Certifications.Application.UseCases.OrdenesCompras.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.ConceptosGastosTipos.Queries
{
    public class GetListaConceptosGastosTiposQuery : IRequest<List<ConceptoGastoTipoDto>>
    {
        public class GetListaConceptosGastosTiposQueryHandler : IRequestHandler<GetListaConceptosGastosTiposQuery, List<ConceptoGastoTipoDto>>
        {
            private readonly IMapper _mapper;
            private readonly IConceptoGastoTipoService _service;
            public GetListaConceptosGastosTiposQueryHandler(IMapper mapper, IConceptoGastoTipoService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<ConceptoGastoTipoDto>> Handle(GetListaConceptosGastosTiposQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllConceptosGastosTipos();

                var coleccion = _mapper.Map<List<ConceptoGastoTipoDto>>(query);

                return coleccion;
            }
        }
    }
}

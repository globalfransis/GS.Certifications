using AutoMapper;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Global.Currencies.Queries;
using MediatR;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries
{
    public class GetCurrenciesQuery : IRequest<List<CurrencyDto>>
    {
        public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, List<CurrencyDto>>
        {
            private readonly IMapper _mapper;
            private readonly IEmpresaPortalService _service;
            public GetCurrenciesQueryHandler(IMapper mapper, IEmpresaPortalService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<CurrencyDto>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllCurrencies();

                var coleccion = _mapper.Map<List<CurrencyDto>>(query);

                return coleccion;
            }
        }
    }
}

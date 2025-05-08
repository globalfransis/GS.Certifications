using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GSF.Application.Security.UserActivities.Dto;
using GSF.Application.Security.UserActivities.Queries;
using GSF.Application.Security.UserActivities.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries
{
    public class GetTiposCuentasQuery : IRequest<List<TipoCuentaDto>>
    {
        public class GetTipoCuentasQueryHandler : IRequestHandler<GetTiposCuentasQuery, List<TipoCuentaDto>>
        {
            private readonly IMapper _mapper;
            private readonly IEmpresaPortalService _service;
            public GetTipoCuentasQueryHandler(IMapper mapper, IEmpresaPortalService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<TipoCuentaDto>> Handle(GetTiposCuentasQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllTiposCuentas();

                var coleccion = _mapper.Map<List<TipoCuentaDto>>(query);

                return coleccion;
            }
        }
    }
}

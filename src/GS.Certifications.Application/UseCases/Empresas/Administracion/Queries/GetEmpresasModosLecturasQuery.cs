using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using MediatR;
using GS.Certifications.Application.Commons.Dtos.ModosLecturas;
using GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;
using GS.Certifications.Domain.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries
{
    public class GetEmpresasModosLecturasQuery : IRequest<List<EmpresaModoLecturaDto>>
    {
        public long? EmpresaPortalId { get; set; }

        public class GetEmpresasModosLecturasQueryHandler : IRequestHandler<GetEmpresasModosLecturasQuery, List<EmpresaModoLecturaDto>>
        {
            private readonly IMapper _mapper;
            private readonly IEmpresaPortalService _service;
            public GetEmpresasModosLecturasQueryHandler(IMapper mapper, IEmpresaPortalService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<EmpresaModoLecturaDto>> Handle(GetEmpresasModosLecturasQuery request, CancellationToken cancellationToken)
            {
                var query = await _service.GetAllEmpresasModosLecturas(request.EmpresaPortalId);

                var coleccion = _mapper.Map<List<EmpresaModoLecturaDto>>(query);

                return coleccion;
            }
        }
    }
}

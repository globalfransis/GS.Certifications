using AutoMapper;
using GS.Certifications.Application.Commons.Dtos.ModosLecturas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;
public class GetModosLecturasQuery : IRequest<List<ModoLecturaDto>>
{
    public class GetModosLecturasQueryHandler : IRequestHandler<GetModosLecturasQuery, List<ModoLecturaDto>>
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaPortalService _service;
        public GetModosLecturasQueryHandler(IMapper mapper, IEmpresaPortalService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<List<ModoLecturaDto>> Handle(GetModosLecturasQuery request, CancellationToken cancellationToken)
        {
            var query = await _service.GetAllModosLecturas();

            var coleccion = _mapper.Map<List<ModoLecturaDto>>(query);

            return coleccion;
        }
    }
}
using AutoMapper;
using GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;
using GSF.Application.Global.Currencies.Queries;
using MediatR;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GS.Certifications.Domain.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries
{
    public class GetUsuariosEmpresasQuery : IRequest<List<UsuarioEmpresaPortalDto>>
    {
        public long TipoUsuario { get; set; }
        public long? UserId { get; set; }
        public long? EmpresaPortalId { get; set; }
        public bool? Habilitado { get; set; }

        public class GetUsuariosEmpresasQueryHandler : IRequestHandler<GetUsuariosEmpresasQuery, List<UsuarioEmpresaPortalDto>>
        {
            private readonly IMapper _mapper;
            private readonly IUsuarioEmpresaPortalService _service;
            public GetUsuariosEmpresasQueryHandler(IMapper mapper, IUsuarioEmpresaPortalService service)
            {
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<UsuarioEmpresaPortalDto>> Handle(GetUsuariosEmpresasQuery request, CancellationToken cancellationToken)
            {
                GetAllRequestDto dto = new()
                {
                    UserId = request.UserId,
                    EmpresaPortalId = request.EmpresaPortalId,
                    Habilitado = request.Habilitado
                };

                List<UsuarioEmpresaPortal> query = (await _service.GetAllAsync(dto)).ToList();

                query = query.Where(u => u.User.UserTypeIdm == request.TipoUsuario && !u.IsDeleted).ToList();

                var coleccion = _mapper.Map<List<UsuarioEmpresaPortalDto>>(query);

                return coleccion;
            }
        }
    }
}

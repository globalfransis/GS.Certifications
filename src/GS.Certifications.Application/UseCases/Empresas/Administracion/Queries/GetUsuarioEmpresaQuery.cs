using AutoMapper;
using GS.Certifications.Application.Commons.Services.UsuarioEmpresaPortales;
using GSF.Application.Extensions.GSFMediatR;
using MediatR;
using GS.Certifications.Domain.Entities.Seguridad;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Empresas.Administracion.Queries;

// Query definition
public class GetUsuarioEmpresaQuery : IRequest<UsuarioEmpresaPortalDto>
{
    public int UsuarioEmpresaPortalId { get; set; }
}

// Query handler definition
public class GetUsuarioEmpresaQueryHandler : BaseRequestHandler<UsuarioEmpresaPortal, GetUsuarioEmpresaQuery, UsuarioEmpresaPortalDto>
{
    private readonly IUsuarioEmpresaPortalService _service;

    public GetUsuarioEmpresaQueryHandler(IUsuarioEmpresaPortalService service, IMapper mapper) : base(mapper)
    {
        _service = service;
    }

    protected override async Task<UsuarioEmpresaPortal>
        HandleRequestAsync(GetUsuarioEmpresaQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.UsuarioEmpresaPortalId);
    }
}

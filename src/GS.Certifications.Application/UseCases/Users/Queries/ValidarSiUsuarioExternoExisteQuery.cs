using AutoMapper;
using GSF.Application.Common.Interfaces;
using GSF.Application.Extensions.GSFMediatR;
using GSF.Application.Services.Security.Users;
using GSF.Domain.Entities.Security;
using MediatR;
using GS.Certifications.Application.Commons.Dtos.Empresas;
using GS.Certifications.Application.UseCases.Empresas.Administracion.Services;
using GS.Certifications.Domain.Commons.Constants;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Certifications.Application.UseCases.Users.Queries;

// Query definition
public class ValidarSiUsuarioExternoExisteQuery : IRequest<bool>
{
    public string Email { get; set; }
}

// Query handler definition
public class ValidarSiUsuarioExternoExisteQueryHandler : BaseRequestHandler<bool, ValidarSiUsuarioExternoExisteQuery, bool>
{
    private readonly IUserService _service;
    private readonly ICurrentDomainService _currentDomainService;

    public ValidarSiUsuarioExternoExisteQueryHandler(IUserService service, ICurrentDomainService currentDomainService, IMapper mapper) : base(mapper)
    {
        _service = service;
        _currentDomainService = currentDomainService;
    }

    protected override async Task<bool> HandleRequestAsync(ValidarSiUsuarioExternoExisteQuery request, CancellationToken cancellationToken)
    {
        User usuario = await _service.GetByEmailAsync(request.Email, UserTypeIdmConstants.Socio);
        return usuario != null;
    }
}

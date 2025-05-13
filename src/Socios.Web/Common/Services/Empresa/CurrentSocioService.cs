using Socios.Web.Common.Services.Empresa.Storage;
using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Seguridad;

namespace Socios.Web.Common.Services.Empresa;

public class CurrentSocioService : ICurrentSocioService
{
    private readonly ICurrentEmpresaPortalRepository _repository;

    private UsuarioEmpresaPortal UsuarioEmpresaPortal { get; set; }
    private EmpresaPortal EmpresaPortal { get; set; }
    private RolTipo RolTipo { get; set; }
    public CurrentSocioService(ICurrentEmpresaPortalRepository repository)
    {
        _repository = repository;
    }

    public long? GetCurrentUsuarioEmpresaPortalId()
    {
        if (UsuarioEmpresaPortal is null) UsuarioEmpresaPortal = _repository.GetUsuarioEmpresaPortal();
        return UsuarioEmpresaPortal?.Id;
    }

    public int? GetCurrentEmpresaPortalId()
    {
        if (UsuarioEmpresaPortal is null) UsuarioEmpresaPortal = _repository.GetUsuarioEmpresaPortal();
        return UsuarioEmpresaPortal?.EmpresaPortalId;
    }

    public long? GetCurrentRolTipoIdm()
    {
        if (RolTipo is null) RolTipo = _repository.GetRolTipo();
        return RolTipo?.Idm;
    }
}
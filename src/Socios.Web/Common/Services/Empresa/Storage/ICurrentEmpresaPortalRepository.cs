using GS.Certifications.Domain.Entities.Empresas;
using GS.Certifications.Domain.Entities.Seguridad;

namespace Socios.Web.Common.Services.Empresa.Storage;

public interface ICurrentEmpresaPortalRepository
{
    public UsuarioEmpresaPortal GetUsuarioEmpresaPortal();
    public void SetUsuarioEmpresaPortal(UsuarioEmpresaPortal usuarioEmpresaPortal);

    public RolTipo GetRolTipo();
    public void SetRolTipo(RolTipo rol);

}
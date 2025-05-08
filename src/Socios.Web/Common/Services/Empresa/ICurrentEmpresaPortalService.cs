namespace Socios.Web.Common.Services.Empresa;

public interface ICurrentEmpresaPortalService
{
    public long? GetCurrentUsuarioEmpresaPortalId();
    public int? GetCurrentEmpresaPortalId();
    public long? GetCurrentRolTipoIdm();
}

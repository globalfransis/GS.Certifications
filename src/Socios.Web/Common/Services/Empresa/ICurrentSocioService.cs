namespace Socios.Web.Common.Services.Empresa;

public interface ICurrentSocioService
{
    public long? GetCurrentUsuarioEmpresaPortalId();
    public int? GetCurrentEmpresaPortalId();
    public long? GetCurrentRolTipoIdm();
}

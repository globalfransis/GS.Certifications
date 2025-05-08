using GS.Certifications.Domain.Entities.Empresas;
using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Seguridad;

public class UsuarioEmpresaPortalRol : BaseEntity
{
    public long UsuarioEmpresaPortalId { get; set; }
    public short RolTipoId { get; set; }
    public RolTipo RolTipo { get; set; }
    public bool Habilitado { get; set; }
}

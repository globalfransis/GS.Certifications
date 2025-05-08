using GS.Certifications.Domain.Entities.Empresas;
using GSF.Domain.Common;

namespace GS.Certifications.Domain.Entities.Empresas.Roles;

public class EmpresaRol : BaseIntEntity
{
    public int EmpresaPortalId { get; set; }
    public short RolTipoId { get; set; }
    public RolTipo RolTipo { get; set; }
}

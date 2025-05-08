using GS.Certifications.Domain.Entities.Empresas;
using GSF.Domain.Entities.Systems;
using System;
using System.Collections.Generic;

namespace GS.Certifications.Domain.Entities.Seguridad;

public class UsuarioEmpresaPortal : UserExterno
{
    public int EmpresaPortalId { get; set; }
    public EmpresaPortal EmpresaPortal { get; set; }
    public DateTime FechaRegistracion { get; set; }
    public List<UsuarioEmpresaPortalRol> Roles { get; set; } = new();
    public bool Habilitado { get; set; }
}
